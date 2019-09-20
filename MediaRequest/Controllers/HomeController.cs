using MediaRequest.Application;
using MediaRequest.Domain;
using MediaRequest.Models;
using MediaRequest.WebUI.Models;
using MediaRequest.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MediaRequest.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediaDbContext _context;

        public HomeController(IMediaDbContext context)
        {
            _context = context;
        }

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

        public async Task<IActionResult> Request(string id)
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

            var movie = await SearchSingleMovie(id);

            //var response = RequestMovie(request);

            return View();
        }

        public async Task<bool> ApproveRequest(int requestId)
        {
            var requestObject = await _context.Request.Select(x => new UserRequest()).SingleOrDefaultAsync(x => x.RequestId == requestId);

            var movie = await SearchSingleMovie(requestObject.MovieId);

            var request = new MovieRequestObject()
            {
                title = movie.Title,
                path = $"/home17/robert/downloads/movies/{movie.Title} ({movie.Year})".Replace(":", ""),
                qualityProfileId = 7,
                year = movie.Year,
                tmdbId = movie.TMDBId,
                titleSlug = movie.TitleSlug,
                images = movie.Images
            };

            var response = await RequestMovie(request);

            return response;
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
                var response = await client.GetAsync($"https://tiger.seedhost.eu/robert/radarr/api/movie?apikey=fc2c71c89e9b42cf99c4bd4d215632b0");
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
                var response = await client.GetAsync($"https://tiger.seedhost.eu/robert/radarr/api/movie/lookup/tmdb?apikey=fc2c71c89e9b42cf99c4bd4d215632b0&tmdbId={id}");
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
                    var response = await client.GetAsync($"https://tiger.seedhost.eu/robert/radarr/api/movie/lookup?apikey=fc2c71c89e9b42cf99c4bd4d215632b0&term={cleanedInput}");
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

        public async Task<bool> RequestMovie(MovieRequestObject obj)
        {
            var state = false;

            if(obj != null)
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

                    var parameters = $"&title={obj.title}&qualityProfileId={obj.qualityProfileId}&titleSlug={obj.titleSlug}&tmdbId={obj.tmdbId}&year={obj.year}&path={obj.path}&images={obj.images}";

                    var response = await client.PostAsync($"https://tiger.seedhost.eu/robert/radarr/api/movie?apikey=fc2c71c89e9b42cf99c4bd4d215632b0", content);

                    //var res = JsonConvert.DeserializeObject<Movie>(response.Content.ReadAsStreamAsync());
                    if(response.StatusCode.ToString().StartsWith("2"))
                    {
                        state = true;
                    } else
                    {
                        state = false;
                    }
                }
            }

            return state;
        }
    }
}
