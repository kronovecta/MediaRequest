﻿using MediaRequest.Application.Queries.Movies;
using MediaRequest.Application.Queries.People.GetPopularMovies;
using MediaRequest.Application.Queries.Requests.GetSingleRequest;
using MediaRequest.Domain;
using MediaRequest.Domain.Interfaces;
using MediaRequest.WebUI.Models.IdentityModels;
using MediaRequest.WebUI.ViewModels;
using MediaRequest.WebUI.ViewModels.SingleMovie;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.Controllers
{
    [Authorize]
    public class MovieController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMemoryCache _memoryCache;

        public MovieController(IMediator mediator, UserManager<ApplicationUser> userManager, IMemoryCache memoryCache)
        {
            _mediator = mediator;
            _userManager = userManager;
            _memoryCache = memoryCache;
        }

        [Route("movie/{slug}")]
        [ResponseCache(Duration = 180, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Show(string slug)
        {
            var result = await _mediator.Send(new GetSingleMovieRequest() { TmdbId = slug.Split('-').Last() });
            var existing = await _mediator.Send(new GetExistingMoviesRequest());
            var currentUser = _userManager.GetUserId(User);

            if (existing.Movies.Any(x => x.TmdbId == result.Movie.TmdbId))
            {
                result.Movie.HasFile = true;
            }

            var requested = await _mediator.Send(new RequestExistsRequest { Movie = result.Movie, UserId = currentUser });

            var model = new MovieViewModel();
            model.Movie = result.Movie;
            model.Requested = requested.Exists;

            return View(model);
        }

        public IActionResult Trailer(string ytid)
        {
            return PartialView("_TrailerPartial", ytid);
        }

        [Route("movie/credits")]
        public async Task<IActionResult> Credits(string tmdbid, int? amount)
        {
            var response = await _mediator.Send(new GetCreditsRequest { TMDBId = tmdbid, Amount = amount ?? 0 });
            var model = new MovieCreditsViewModel(response.Credits) { TMDBId = tmdbid };

            return PartialView("_CreditsPartial", model);
        }

        [Route("movie/recommendations")]
        public async Task<IActionResult> Recommendations(string tmdbid)
        {
            var response = await _mediator.Send(new GetRecommendedRequest { TMDBId = tmdbid, Page = 1 });
            var model = response.Recommendations;
            model.TmdbId = tmdbid;

            return PartialView("_RecommendationsPartial", model);
        }

        //[Route("actor/{actorslug}")]
        //public async Task<IActionResult> Actor(string actorslug)
        //{
        //    var actorId = actorslug.Split("-").Last();

        //    var response = await _mediator.Send(new GetCombinedMediaRequest { ActorID = actorId });
        //    //var movies = await _mediator.Send(new GetPopularMoviesRequest { ActorId = actorId });

        //    //var m = movies.Movies.ToList().GetRange(0, (movies.Movies.Count() > 9 ? 9 : movies.Movies.Count())).Select(x => new Movie
        //    //{
        //    //    Id = x.Id,
        //    //    Title = x.Title,
        //    //    FanartUrl = x.BackdropPath ?? "https://www.themoviedb.org/assets/2/v4/glyphicons/basic/glyphicons-basic-38-picture-grey-c2ebdbb057f2a7614185931650f8cee23fa137b93812ccb132b9df511df1cfac.svg",
        //    //    Overview = x.Overview
        //    //});

        //    var previousWorks = await _mediator.Send(new GetCombinedCreditsRequest(actorId));

        //    var model = new ActorViewModel
        //    {
        //        Actor = response.Actor,
        //        PreviousWorks = new CombinedCreditsViewModel
        //        {
        //            Cast = previousWorks.Credits.Cast,
        //            Crew = previousWorks.Credits.Crew
        //        }
        //    };

        //    return View(model);
        //}

        private async Task<IEnumerable<IMediaType>> GetPreviousWorks(string actorId)
        {
            var movies = await _mediator.Send(new GetPopularMoviesRequest { ActorId = actorId, MediaType = MediaType.Movie });
            var series = await _mediator.Send(new GetPopularMoviesRequest { ActorId = actorId, MediaType = MediaType.Tv });

            var joined = new List<IMediaType>();
            joined.Concat(movies.Movies as IEnumerable<IMediaType>);
            joined.Concat(series.Movies as IEnumerable<IMediaType>);

            //joined.OrderBy(x => x.)

            return joined;
        }
    }
}