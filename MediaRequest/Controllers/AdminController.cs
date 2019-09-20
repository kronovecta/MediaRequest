using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MediaRequest.Application;
using MediaRequest.Application.Queries;
using MediaRequest.Data;
using MediaRequest.Domain;
using MediaRequest.WebUI.Models;
using MediaRequest.WebUI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MediaRequest.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMediaDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(IMediator mediator, IMediaDbContext context, UserManager<IdentityUser> userManager)
        {
            _mediator = mediator;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> AdminPanel()
        {
            var modelList = new List<MovieUserRequestViewModel>();

            var requests = _context.Request.ToList();

            foreach (var request in requests)
            {
                var movieRequest = new GetSingleMovieRequest()
                {
                    TmdbId = request.MovieId
                };

                var response = await _mediator.Send(movieRequest);

                var model = new MovieUserRequestViewModel()
                {
                    Movie = response.Movie,
                    User = await _userManager.FindByIdAsync(request.UserId),
                    Request = request
                };

                modelList.Add(model);
            }

            return View(modelList);
        }

        public async Task<bool> ApproveRequest(int requestId)
        {
            var requestObject = await _context.Request.Select(x => new UserRequest()).SingleOrDefaultAsync(x => x.RequestId == requestId);

            //var movie = await SearchSingleMovie(requestObject.MovieId);

            var movie = await _mediator.Send(new GetSingleMovieRequest() { TmdbId = requestObject.MovieId });

            var request = new MovieRequestObject()
            {
                title = movie.Movie.Title,
                path = $"/home17/robert/downloads/movies/{movie.Movie.Title} ({movie.Movie.Year})".Replace(":", ""),
                qualityProfileId = 7,
                year = movie.Movie.Year,
                tmdbId = movie.Movie.TMDBId,
                titleSlug = movie.Movie.TitleSlug,
                images = movie.Movie.Images
            };

            var response = await RequestMovie(request);

            if (response)
            {
                await _context.Request.SingleOrDefaultAsync(x => x.Status == true);
                await _context.SaveChangesAsync();
            }

            return response;
        }

        public async Task<bool> RequestMovie(MovieRequestObject obj)
        {
            var state = false;

            if (obj != null)
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

                    var parameters = $"&title={obj.title}&qualityProfileId={obj.qualityProfileId}&titleSlug={obj.titleSlug}&tmdbId={obj.tmdbId}&year={obj.year}&path={obj.path}&images={obj.images}";

                    var response = await client.PostAsync($"https://tiger.seedhost.eu/robert/radarr/api/movie?apikey=<API_KEY>", content);

                    //var res = JsonConvert.DeserializeObject<Movie>(response.Content.ReadAsStreamAsync());
                    if (response.StatusCode.ToString().StartsWith("2"))
                    {
                        state = true;
                    }
                    else
                    {
                        state = false;
                    }
                }
            }

            return state;
        }
    }
}