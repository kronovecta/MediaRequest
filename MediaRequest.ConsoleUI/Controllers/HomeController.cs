using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediaRequest.ConsoleUI.Models;
using System.Net.Http;
using Newtonsoft.Json;
using MediaRequest.WebUI.Models.API_Objects;
using MediaRequest.WebUI.ViewModels;

namespace MediaRequest.ConsoleUI.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var movies = await FetchAddedMovies();
            return View(movies);
        }

        public IActionResult Search()
        {
            var model = new SearchViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SearchMoviesByName(SearchViewModel query)
        {
            var model = new List<Movie>();
            model = await SearchMovies(query.Input);

            return PartialView("_MovieSearchResultPartial", model);
        }

        public async Task<IActionResult> Request(string tmdbid)
        {
            //var request = new MovieRequestObject()
            //{
            //    title = "Godzilla King of the Monsters",
            //    path = "/home17/robert/downloads/movies/Godzilla King of the Monsters (2019)",
            //    qualityProfileId = 7,
            //    year = 2019,
            //    tmdbId = 373571,
            //    titleSlug = "godzilla-king-of-the-monsters-373571"
            //};

            var movie = await SearchSingleMovie(tmdbid);

            var request = new MovieRequestObject()
            {
                title = movie.Title,
                path = "/home17/robert/downloads/movies/Godzilla King of the Monsters (2019)",
                qualityProfileId = 7,
                year = movie.Year,
                tmdbId = movie.TMDBId,
                titleSlug = movie.TitleSlug,
                images = movie.Images
            };

            using (var client = new HttpClient())
            {

            }

            return View();
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

        public async Task<IEnumerable<Movie>> FetchAddedMovies()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"https://tiger.seedhost.eu/robert/radarr/api/movie?apikey=0c3f40a77c5b4c47b8b4dbc8b80ebf52");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<IEnumerable<Movie>>(result);

                return json;
            }
        }

        public async Task<Movie> SearchSingleMovie(string id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"https://tiger.seedhost.eu/robert/radarr/api/movie/lookup/tmdb?apikey=0c3f40a77c5b4c47b8b4dbc8b80ebf52&tmdbId=373571");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<Movie>(result);

                return json;
            }
        }

        public async Task<List<Movie>> SearchMovies(string input)
        {
            var cleanedInput = "";
            if(input != null && input != "")
            {
                cleanedInput = input.Replace(" ", "%20");

                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"https://tiger.seedhost.eu/robert/radarr/api/movie/lookup?apikey=0c3f40a77c5b4c47b8b4dbc8b80ebf52&term={cleanedInput}");
                    response.EnsureSuccessStatusCode();

                    var result = await response.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject<List<Movie>>(result);

                    return json;
                }
            } else
            {
                return null;
            }
        }
    }
}
