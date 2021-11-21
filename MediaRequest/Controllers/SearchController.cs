using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaRequest.Application.Queries.Movies;
using MediaRequest.Application.Queries.Movies.SearchMovieByName;
using MediaRequest.WebUI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediaRequest.WebUI.Controllers
{
    public class SearchController : Controller
    {
        private readonly IMediator _mediator;

        public SearchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            var model = new SearchResultViewModel();

            return View(model);
        }

        public async Task<IActionResult> SearchMoviesByName(string term)
        {
             var request = new SearchMovieByNameRequest
            {
                SearchTerm = term,
            };

            var results = await _mediator.Send(request);

            var existingMovies = await _mediator.Send(new GetExistingMoviesRequest());

            var model = new SearchResultViewModel();

            foreach (var item in results.Movies)
            {
                var movieExists = new MovieExists();
                var existingMovie = existingMovies.Movies.SingleOrDefault(x => x.TmdbId == item.TmdbId);

                if (existingMovie != null)
                {
                    movieExists.Downloaded = existingMovie.MovieFile != null;
                    movieExists.Monitored = existingMovie.Monitored;
                    movieExists.Exists = true;
                    movieExists.Movie = item;
                }
                else
                {
                    movieExists.Exists = false;
                    movieExists.Movie = item;
                }

                model.Movies.Add(movieExists);
            }

            model.LatestMovie = existingMovies.LatestMovie;

            return PartialView("_MovieSearchResultPartial", model);
        }
    }
}