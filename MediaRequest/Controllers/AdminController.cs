using MediaRequest.Application;
using MediaRequest.Application.Commands.ApproveRequest;
using MediaRequest.Application.Queries;
using MediaRequest.Application.Queries.Movies;
using MediaRequest.Application.Queries.Movies.GetUpcoming;
using MediaRequest.Application.Queries.Requests;
using MediaRequest.Domain.Configuration;
//using MediaRequest.WebUI.Models.Configuration;
using MediaRequest.WebUI.ViewModels;
using MediaRequest.WebUI.ViewModels.Admin;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMediaDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApiKeys _apikeys;

        public AdminController(IMediator mediator, IMediaDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<ApiKeys> apikeys)
        {
            _mediator = mediator;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _apikeys = apikeys.Value;
        }

        public async Task<IActionResult> AdminPanel()
        {
            var model = new AdminPanelViewModel();

            var upcomingResponse = await _mediator.Send(new GetUpcomingRequest());
            var requests = await _mediator.Send(new GetRequestsRequest());
            var members = await _userManager.Users.CountAsync();
            var existingMovies = await _mediator.Send(new GetExistingMoviesRequest());

            model.Requests = requests.Requests.Count();
            model.Members = members;
            model.LatestRequest = requests.Requests.Last();
            model.Reminders = 0;
            model.TotalMovies = existingMovies.Movies.Count();

            if (upcomingResponse.Movies.Count() > 0)
            {
                model.NextUpcomingMovie = upcomingResponse.Movies.First();
            } else
            {
                model.NextUpcomingMovie = null;
            }

            return View(model);
        }

        public async Task<IActionResult> Requests()
        {
            var modelList = new List<MovieUserRequestViewModel>();

            var requests = await _mediator.Send(new GetRequestsRequest());

            foreach (var request in requests.Requests)
            {
                var movieRequest = new GetSingleMovieRequest()
                {
                    TmdbId = request.MovieId
                };

                var response = await _mediator.Send(movieRequest);

                var model = new MovieUserRequestViewModel()
                {
                    Movie = response.Movie,
                    User = await _userManager.FindByIdAsync(request.UserId.ToString()),
                    Request = request
                };

                modelList.Add(model);
            }

            return View(modelList);
        }

        public async Task<IActionResult> ApproveRequest(int requestId)
        {
            var userRequest = await _context.Request.SingleOrDefaultAsync(x => x.Id == requestId);

            var movie = await _mediator.Send(new GetSingleMovieRequest() { TmdbId = userRequest.MovieId });

            var request = new Domain.MovieRequestObject()
            {
                title = movie.Movie.Title,
                path = $"/home17/robert/downloads/movies/{movie.Movie.Title} ({movie.Movie.Year})".Replace(":", ""),
                qualityProfileId = 7,
                year = movie.Movie.Year,
                tmdbId = movie.Movie.TMDBId,
                titleSlug = movie.Movie.TitleSlug,
                images = movie.Movie.Images
            };

            var command = new ApproveRequestCommand { ApiKey = _apikeys.Radarr, RequestObject = request };
            var result = await _mediator.Send(command);

            if (result == true)
            {
                userRequest.Status = true;
                await _context.SaveChangesAsync();
            } else
            {
                userRequest.Status = false;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("AdminPanel", "Admin");
        }

        //[Route("/user/manage")]
        public async Task<IActionResult> UserManager()
        {
            var users = _userManager.Users.ToList();
            var model = new UserManagerViewModel();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var item = new UserRoleViewModel { Roles = roles, User = user };

                model.Users.Add(item);
            }

            return View(model);
        }
    }
}