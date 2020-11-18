using MediaRequest.Application;
using MediaRequest.Application.Commands;
using MediaRequest.Application.Queries.Movies;
using MediaRequest.Application.Queries.Movies.SearchMovieByName;
using MediaRequest.Domain;
using MediaRequest.Domain.Configuration;
using MediaRequest.Domain.Radarr;
using MediaRequest.Models;
using MediaRequest.WebUI.Models.IdentityModels;
//using MediaRequest.WebUI.Models.Configuration;
using MediaRequest.WebUI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _mediator.Send(new GetExistingMoviesRequest() { Amount = 10 });

            var model = new IndexViewModel()
            {
                LatestMovie = movies.LatestMovie,
                PartialView = new IndexListPartialViewModel
                {
                    Term = "",
                    FilterMode = 0,
                    Movies = movies.Movies,
                    TotalPages = movies.TotalPages,
                    CurrentPage = movies.CurrentPage
                }
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string term, int filter, int? pagenr)
        {
            pagenr = pagenr - 1;

            if(term == null && filter == 0)
            {
                var response = await _mediator.Send(new GetExistingMoviesRequest() { CurrentPage = pagenr ?? 0, Amount = 10 } );

                var model = new IndexViewModel()
                {
                    LatestMovie = response.LatestMovie,
                    PartialView = new IndexListPartialViewModel
                    {
                        Term = term,
                        FilterMode = filter,
                        Movies = response.Movies,
                        TotalPages = response.TotalPages,
                        CurrentPage = response.CurrentPage
                    }
                };

                return PartialView("_MovieListPartial", model.PartialView);
            } 
            else
            {
                var response = await _mediator.Send(new GetExistingMoviesFilteredRequest() { Input = term, FilterMode = filter, CurrentPage = pagenr ?? 0 });

                var model = new IndexViewModel()
                {
                    LatestMovie = response.LatestMovie,
                    PartialView = new IndexListPartialViewModel
                    {
                        Term = term,
                        FilterMode = filter,
                        Movies = response.Movies,
                        TotalPages = response.TotalPages,
                        CurrentPage = response.CurrentPage
                    }
                };

                return PartialView("_MovieListPartial", model.PartialView);
            }
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
