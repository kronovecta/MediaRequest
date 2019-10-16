using MediaRequest.Application;
using MediaRequest.Application.Commands;
using MediaRequest.Application.Queries.Movies;
using MediaRequest.Application.Queries.Movies.SearchMovieByName;
using MediaRequest.Domain;
using MediaRequest.Domain.Configuration;
using MediaRequest.Domain.Radarr;
using MediaRequest.Models;
//using MediaRequest.WebUI.Models.Configuration;
using MediaRequest.WebUI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MediaRequest.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediaDbContext _context;
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(IMediaDbContext context, IMediator mediator, IOptions<ApiKeys> apikeys, UserManager<IdentityUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _mediator.Send(new GetExistingMoviesRequest());

            var model = new IndexViewModel() 
            {
                Movies = movies.Movies,
                LatestMovie = movies.LatestMovie
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string term)
        {
            if(term == null)
            {
                var response = await _mediator.Send(new GetExistingMoviesRequest());
                var model = response.Movies;

                return PartialView("_MovieListPartial", model);
            } 
            else
            {
                var movies = await _mediator.Send(new GetExistingMoviesFilteredRequest() { Input = term });

                var model = new IndexViewModel()
                {
                    Movies = movies.Movies,
                    LatestMovie = movies.LatestMovie
                };

                return PartialView("_MovieListPartial", model.Movies);
            }
        }

        public IActionResult Search()
        {
            //var isAjax = HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            var model = new SearchResultViewModel();

            return View(model);
        }

        //public async Task<IActionResult> SearchMoviesByName(SearchResultViewModel inputModel)
        //{
        //    var request = new SearchMovieByNameRequest
        //    {
        //        SearchTerm = inputModel.SearchTerm,
        //        ApiKey_Radarr = _apikeys.Radarr,
        //        ApiKey_TMDB = _apikeys.TMDB
        //    };

        //    var results = await _mediator.Send(request);

        //    var existingMovies = await _mediator.Send(new GetExistingMoviesRequest { ApiKey_Radarr = _apikeys.Radarr });

        //    var model = new SearchResultViewModel();

        //    foreach (var item in results.Movies)
        //    {
        //        var movieExists = new MovieExists();
        //        var existingMovie = existingMovies.Movies.SingleOrDefault(x => x.TMDBId == item.TMDBId);

        //        if (/*existingMovies.Movies.Any(x => x.Title == item.Title)*/ existingMovie != null)
        //        {
        //            movieExists.Downloaded = existingMovie.Downloaded;
        //            movieExists.Monitored = existingMovie.Monitored;
        //            movieExists.Exists = true;
        //            movieExists.Movie = item;
        //        }
        //        else
        //        {
        //            movieExists.Exists = false;
        //            movieExists.Movie = item;
        //        }

        //        model.Movies.Add(movieExists);
        //    }

        //    model.LatestMovie = existingMovies.LatestMovie;

        //    return View(model);
        //}

        //[HttpPost]
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
                var existingMovie = existingMovies.Movies.SingleOrDefault(x => x.TMDBId == item.TMDBId);

                if (/*existingMovies.Movies.Any(x => x.Title == item.Title)*/ existingMovie != null)
                {
                    movieExists.Downloaded = existingMovie.Downloaded;
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

        public async Task<IActionResult> Request(string tmdbid)
        {
            var requests = await _context.Request.ToListAsync();
            bool valid = false;

            if (!requests.Any(x => x.MovieId == tmdbid)) valid = true;

            if (valid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var request = new UserRequest() { Status = false, MovieId = tmdbid, UserId = currentUser.Id };

                var command = new AddRequestCommand { Request = request };

                await _mediator.Send(command);
            }

            return RedirectToAction("Search", "Home");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
