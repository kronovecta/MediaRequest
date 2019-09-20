using MediaRequest.Application;
using MediaRequest.Application.Commands;
using MediaRequest.Application.Queries;
using MediaRequest.Domain;
using MediaRequest.Models;
using MediaRequest.WebUI.Models;
using MediaRequest.WebUI.ViewModels;
using MediatR;
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
        private readonly IMediator _mediator;

        public HomeController(IMediaDbContext context, IMediator mediator)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //var movies = await FetchAddedMovies();
            var model = new MoviesMovieUserViewModel { Model = new MovieUserViewModel(), Movies = await FetchAddedMovies() };

            return View(model);
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

        public async Task<IActionResult> Request(string id, string userid)
        {
            var request = new UserRequest { MovieId = id, UserId = userid, Status = false };

            var command = new AddRequestCommand { Request = request };

            await _mediator.Send(command);

            //var request = new MovieRequestObject()
            //{
            //    title = "Godzilla King of the Monsters",
            //    path = "/home17/robert/downloads/movies/Godzilla King of the Monsters (2019)",
            //    qualityProfileId = 7,
            //    year = 2019,
            //    tmdbId = 373571,
            //    titleSlug = "godzilla-king-of-the-monsters-373571"
            //};

            //var movie = await SearchSingleMovie(id);

            //var response = RequestMovie(request);

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
                var response = await client.GetAsync($"https://tiger.seedhost.eu/robert/radarr/api/movie?apikey=<API_KEY>");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<IEnumerable<Movie>>(result);

                return json;
            }
        }

        //public async Task<Movie> SearchSingleMovie(string id)
        //{
        //}

        public async Task<List<Movie>> SearchMovies(string input)
        {
            var cleanedInput = "";
            if(input != null && input != "")
            {
                cleanedInput = input.Replace(" ", "%20");

                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"https://tiger.seedhost.eu/robert/radarr/api/movie/lookup?apikey=<API_KEY>&term={cleanedInput}");
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
