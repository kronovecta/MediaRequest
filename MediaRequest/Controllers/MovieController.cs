using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediaRequest.Application.Queries;
using MediaRequest.Domain.Configuration;
using MediaRequest.Domain.Radarr;
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
            //_apikeys.Radarr = _configuration["ApiKeys:Radarr"];
            //_apikeys.Radarr = _configuration["ApiKeys:TMDB"];

            //var movie = JsonConvert.DeserializeObject<Movie>(System.IO.File.ReadAllText("../radarr_single_movie_johnwick3.json"));
            var result = await _mediator.Send(new GetSingleMovieRequest() { Keys = _apikeys, TmdbId = tmdbid });

            return View(result.Movie);
        }
    }
}