using MediaRequest.Application.Queries.Movies;
using MediaRequest.Application.Queries.Movies.GetHistory;
using MediaRequest.Application.Queries.Movies.GetSingleExistingMovie;
using MediaRequest.Application.Queries.Television.Sonarr;
using MediaRequest.Domain.API_Responses.Shared;
using MediaRequest.Domain.Radarr;
using MediaRequest.Models;
using MediaRequest.WebUI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace MediaRequest.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memoryCache;

        public HomeController(IMediator mediator, IMemoryCache memoryCache)
        {
            
            _mediator = mediator;
            _memoryCache = memoryCache;
        }

        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Index()
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60));

            var existingMovies = new GetExistingMoviesResponse();
            var radarrHistory = new GetHistoryResponse();

            var latestEpisode = new GetLatestEpisodeResponse();

            if (!_memoryCache.TryGetValue("_ExistingMovies", out existingMovies))
            {
                existingMovies = await _mediator.Send(new GetExistingMoviesRequest() { Amount = 10 });
                _memoryCache.Set("_ExistingMovies", existingMovies, cacheEntryOptions);
            }

            if (!_memoryCache.TryGetValue("_LatestMovie", out radarrHistory))
            {
                radarrHistory = await _mediator.Send(new GetHistoryRequest { Order = Order.desc, Page = 1, PageSize = 20 });
                _memoryCache.Set("_LatestMovie", radarrHistory.History, cacheEntryOptions);
            }

            if (!_memoryCache.TryGetValue("_LatestEpisode", out latestEpisode))
            {
                latestEpisode = await _mediator.Send(new GetLatestEpisodeRequest() { Order = Order.desc, PageSize = 1 });
                _memoryCache.Set("_LatestEpisode", latestEpisode.History, cacheEntryOptions);
            }

            var latestMovie = await _mediator.Send(new GetSingleExistingMovieRequest { RadarrMovieId = radarrHistory.History.Records.FirstOrDefault(x => x.EventType == "downloadFolderImported").MovieId.ToString() });

            var model = new IndexViewModel()
            {
                LatestMovie = latestMovie.Movie,
                LatestSeries = latestEpisode.History,
                PartialView = new IndexListPartialViewModel
                {
                    Term = "",
                    FilterMode = 0,
                    Movies = existingMovies.Movies,
                    TotalPages = existingMovies.TotalPages,
                    CurrentPage = existingMovies.CurrentPage
                }
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string term, int filter, int? pagenr)
        {
            pagenr = pagenr - 1;
            GetExistingMoviesResponse response = new GetExistingMoviesResponse();

            if(term == null && filter == 0)
            {
                response = await _mediator.Send(new GetExistingMoviesRequest() { CurrentPage = pagenr ?? 0, Amount = 10 });

                //if(!_memoryCache.TryGetValue("_ExistingMovies", out response))
                //{
                //    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60));

                //    _memoryCache.Set("_ExistingMovies", response, cacheEntryOptions);
                //}

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
                 response = await _mediator.Send(new GetExistingMoviesFilteredRequest() { Input = term, FilterMode = filter, CurrentPage = pagenr ?? 0 });

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
