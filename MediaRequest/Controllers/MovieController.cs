using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediaRequest.Domain.Configuration;
using MediaRequest.Domain.Radarr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace MediaRequest.WebUI.Controllers
{
    public class MovieController : Controller
    {
        private readonly IOptions<ApiKeys> _apikeys;

        public MovieController(IOptions<ApiKeys> apikeys)
        {
            _apikeys = apikeys;
        }

        public IActionResult ShowMovie(string tmdbid)
        {
            var movie = JsonConvert.DeserializeObject<Movie>(System.IO.File.ReadAllText("../radarr_single_movie_johnwick3.json"));


            return View(movie);
        }
    }
}