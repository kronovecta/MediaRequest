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

        [Route("movie/{slug}")]
        public async Task<IActionResult> ShowMovie(string slug)
        {
            var result = await _mediator.Send(new GetSingleMovieRequest() { TmdbId = slug.Split('-').Last() });

            return View(result.Movie);
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

        public async Task<IActionResult> Recommendations(string tmdbid, int? page)
        {
            var response = await _mediator.Send(new GetRecommendedRequest { TMDBId = tmdbid, Page = page ?? 1 });
            var model = response.Recommendations;

            return PartialView("_RecommendationsPartial", model);
        }
    }
}