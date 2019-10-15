using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediaRequest.Application.Queries;
using MediaRequest.Application.Queries.Movies;
using MediaRequest.Domain.Configuration;
using MediaRequest.Domain.Radarr;
using MediaRequest.WebUI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace MediaRequest.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApiKeys _apikeys;
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public MovieController(IOptions<ApiKeys> apikeys, IMediator mediator, IConfiguration configuration)
        {
            _configuration = configuration;
            _apikeys = apikeys.Value;
            _mediator = mediator;
        }

        public async Task<IActionResult> ShowMovie(string tmdbid)
        {
            var result = await _mediator.Send(new GetSingleMovieRequest() { TmdbId = tmdbid });

            return View(result.Movie);
        }

        public async Task<IActionResult> Details(string tmdbid)
        {
            return PartialView("_DetailsPartial");
        }

        public async Task<IActionResult> Credits(string tmdbid, int? amount)
        {
            var response = await _mediator.Send(new GetCreditsRequest { TMDBId = tmdbid, Amount = amount ?? 0 });

            var model = new MovieCreditsViewModel { Credits = response.Credits, TMDBId = tmdbid };

            return PartialView("_CreditsPartial", model);
        }

        public async Task<IActionResult> Recommendations(string tmdbid, int? page)
        {
            var response = await _mediator.Send(new GetRecommendedRequest { TMDBId = tmdbid, Page = page ?? 1 });
            var model = response.Recommendations;

            return PartialView("_RecommendationsPartial", model);
        }
    }
}