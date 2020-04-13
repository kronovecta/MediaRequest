using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediaRequest.Application.Queries;
using MediaRequest.Application.Queries.Movies;
using MediaRequest.Application.Queries.People.GetCombinedMedia;
using MediaRequest.Application.Queries.People.GetPopularMovies;
using MediaRequest.Application.Queries.Requests.GetSingleRequest;
using MediaRequest.Domain.Configuration;
using MediaRequest.Domain.Radarr;
using MediaRequest.WebUI.Models.IdentityModels;
using MediaRequest.WebUI.ViewModels;
using MediaRequest.WebUI.ViewModels.SingleMovie;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace MediaRequest.Controllers
{
    [Authorize]
    public class MovieController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public MovieController(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Route("movie/{slug}")]
        public async Task<IActionResult> ShowMovie(string slug)
        {
            var result = await _mediator.Send(new GetSingleMovieRequest() { TmdbId = slug.Split('-').Last() });
            var existing = await _mediator.Send(new GetExistingMoviesRequest());
            var currentUser = _userManager.GetUserId(User);

            if (existing.Movies.Any(x => x.TMDBId == result.Movie.TMDBId))
            {
                result.Movie.AlreadyAdded = true;
            }

            var requested = await _mediator.Send(new RequestExistsRequest { Movie = result.Movie, UserId = currentUser });

            var model = new MovieViewModel();
            model.Movie = result.Movie;
            model.Requested = requested.Exists;

            return View(model);
        }

        public async Task<IActionResult> Trailer(string ytid)
        {
            return PartialView("_TrailerPartial", ytid);
        }

        public async Task<IActionResult> Credits(string tmdbid, int? amount)
        {
            var response = await _mediator.Send(new GetCreditsRequest { TMDBId = tmdbid, Amount = amount ?? 0 });

            var model = new MovieCreditsViewModel { Credits = response.Credits, TMDBId = tmdbid };

            return PartialView("_CreditsPartial", model);
        }

        public async Task<IActionResult> Recommendations(string tmdbid)
        {
            var response = await _mediator.Send(new GetRecommendedRequest { TMDBId = tmdbid, Page = 1 });
            var model = response.Recommendations;
            model.TmdbId = tmdbid;

            return PartialView("_RecommendationsPartial", model);
        }

        public async Task<IActionResult> Actor(string actorid)
        {
            var response = await _mediator.Send(new GetCombinedMediaRequest { ActorID = actorid });
            var movies = await _mediator.Send(new GetPopularMoviesRequest { ActorId = actorid });

            var m = movies.Movies.ToList().GetRange(0, (movies.Movies.Count() > 9 ? 9 : movies.Movies.Count())).Select(x => new Movie
            {
                Id = x.id,
                Title = x.title,
                TitleSlug = x.title.Replace(" ", "-") + x.id,
                FanartUrl = x.backdrop_path ?? "https://www.themoviedb.org/assets/2/v4/glyphicons/basic/glyphicons-basic-38-picture-grey-c2ebdbb057f2a7614185931650f8cee23fa137b93812ccb132b9df511df1cfac.svg",
                Overview = x.overview
            });

            var model = new ActorViewModel
            {
                Actor = response.Actor,
                PopularMovies = m
            };

            return View(model);
        }
    }
}