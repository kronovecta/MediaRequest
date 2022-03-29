using MediaRequest.Application.Business.Enums;
using MediaRequest.Application.Queries.Movies;
using MediaRequest.Application.Queries.Movies.GetHistory;
using MediaRequest.Application.Queries.Movies.GetSingleExistingMovie;
using MediaRequest.Application.Queries.Television;
using MediaRequest.Application.Queries.Television.Sonarr;
using MediaRequest.Domain.API_Responses.Shared;
using MediaRequest.Domain.Configuration;
using MediaRequest.Domain.Interfaces;
using MediaRequest.Domain.Radarr;
using MediaRequest.Models;
using MediaRequest.WebUI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.FeatureManagement;
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
        private readonly IFeatureManager featureManager;
        private readonly MemoryCacheEntryOptions _cacheEntryOptions;

        private readonly int _takeAmount = 10;

        public HomeController(IMediator mediator, IMemoryCache memoryCache, IFeatureManager featureManager)
        {
            
            _mediator = mediator;
            _memoryCache = memoryCache;
            this.featureManager = featureManager;
            _cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(300));
        }

        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Index()
        {
            #region Cache Response

            var existingMovies = await GetResponseAndSaveToCache(new GetExistingMoviesRequest() { Amount = _takeAmount });
            var radarrHistory = await GetResponseAndSaveToCache(new GetHistoryRequest { Order = Order.desc, Page = 1, PageSize = 20 });
            var latestEpisode = await GetResponseAndSaveToCache(new GetLatestEpisodeRequest() { Order = Order.desc, PageSize = 1 });

            #endregion

            var first = new List<HistoryBase>() 
            { 
                radarrHistory.History.Records.FirstOrDefault(), 
                latestEpisode.History.Records.FirstOrDefault() 
            }.OrderByDescending(x => x.Date).FirstOrDefault();

            var lastUpdated = new MediaBase();

            if(first is Domain.API_Responses.Radarr.v3.Record)
            {
                var latest = await _mediator.Send(new GetSingleExistingMovieRequest { 
                    RadarrMovieId = radarrHistory.History.Records.FirstOrDefault(x => x.EventType.Equals(ApiConstants.Imported)).MovieId.ToString() 
                });

                lastUpdated = latest.Movie as MediaBase;
            } else
            {
                var latest = await _mediator.Send(new GetSingleSeriesRequest(latestEpisode.History.Records.FirstOrDefault(x => x.EventType.Equals(ApiConstants.Imported)).SeriesId));
                lastUpdated = latest.TvShow as MediaBase;
            }

            var model = new IndexViewModel()
            {
                LatestMovie = lastUpdated,
                LatestSeries = latestEpisode.History,
                PartialView = new IndexListPartialViewModel
                {
                    Movies = existingMovies.Movies.Take(_takeAmount),
                    TotalPages = existingMovies.TotalPages,
                    CurrentPage = existingMovies.CurrentPage,
                }
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string term, Filters filter, int? pagenr) 
        { 
            var response = new GetExistingMoviesResponse();

            response = await _mediator.Send(new GetExistingMoviesRequest() { Input = term, FilterMode = filter, CurrentPage = pagenr ?? 0, Amount = _takeAmount });

            var model = new IndexViewModel()
            {
                LatestMovie = response.LatestMovie,
                PartialView = new IndexListPartialViewModel
                {
                    Term = term,
                    FilterMode = filter,
                    Movies = response.Movies,
                    TotalPages = response.TotalPages,
                    CurrentPage = pagenr ?? 0
                }
            };

            return PartialView("_MovieListPartial", model.PartialView);
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
        private async Task<IRequestResponse> GetResponseAndSaveToCache<IRequestResponse>(IRequest<IRequestResponse> request)
        {
            IRequestResponse responseObject;
            var cacheExists = _memoryCache.TryGetValue(request.GetType().Name, out responseObject);
            if (!cacheExists)
            {
                responseObject = await _mediator.Send(request);
                _memoryCache.Set(request.GetType().Name, responseObject, _cacheEntryOptions);
            }

            return responseObject;
        }
    }
}
