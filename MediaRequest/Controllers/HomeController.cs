using MediaRequest.Application;
using MediaRequest.Application.Commands;
using MediaRequest.Domain;
using MediaRequest.Models;
using MediaRequest.WebUI.Models.Configuration;
using MediaRequest.WebUI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class HomeController : Controller
    {
        private readonly IMediaDbContext _context;
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApiKeys _apikeys;

        public HomeController(IMediaDbContext context, IMediator mediator, IOptions<ApiKeys> apikeys, UserManager<IdentityUser> userManager)
        {
            _apikeys = apikeys.Value;
            _mediator = mediator;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
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

        public async Task<IActionResult> Request(string tmdbid)
        {
            var requests = await _context.Request.ToListAsync();
            bool valid = false;

            if (!requests.Any(x => x.MovieId == tmdbid)) valid = true;

            if (valid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var request = new UserRequest() { Status = false, MovieId = tmdbid, UserId = Guid.Parse(currentUser.Id) };

                var command = new AddRequestCommand { Request = request };

                await _mediator.Send(command);
            }

            return RedirectToAction("Search", "Home");
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
                var response = await client.GetAsync($"https://tiger.seedhost.eu/robert/radarr/api/movie?apikey={_apikeys.Radarr}");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<IEnumerable<Movie>>(result);

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
                    var response = await client.GetAsync($"https://tiger.seedhost.eu/robert/radarr/api/movie/lookup?apikey={_apikeys.Radarr}&term={cleanedInput}");
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
