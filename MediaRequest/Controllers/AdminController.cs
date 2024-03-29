﻿using MediaRequest.Application;
using MediaRequest.Application.Commands.ApproveRequest;
using MediaRequest.Application.Queries;
using MediaRequest.Application.Queries.Movies;
using MediaRequest.Application.Queries.Movies.GetUpcoming;
using MediaRequest.Application.Queries.Requests;
using MediaRequest.Domain.Configuration;
using MediaRequest.WebUI.Models.IdentityModels;
//using MediaRequest.WebUI.Models.Configuration;
using MediaRequest.WebUI.ViewModels;
using MediaRequest.WebUI.ViewModels.Admin;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMediaDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApiKeys _apikeys;
        private readonly ServicePath _path;

        public AdminController(IOptions<ServicePath> path, IMediator mediator, IMediaDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<ApiKeys> apikeys)
        {
            _mediator = mediator;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _apikeys = apikeys.Value;
            _path = path.Value;
        }

        [Route("admin")]
        public async Task<IActionResult> AdminPanel()
        {
            var upcomingResponse = await _mediator.Send(new GetUpcomingRequest());
            var requests = await _mediator.Send(new GetRequestsRequest());
            var members = await _userManager.Users.CountAsync();
            var existingMovies = await _mediator.Send(new GetExistingMoviesRequest());
                
            var model = new AdminPanelViewModel
            {
                NextUpcomingMovie = upcomingResponse.Movies.Count() > 0 ? upcomingResponse.Movies.First() : null,
                Requests = requests.Requests.Count(),
                Members = members,
                Reminders = 0,
                TotalMovies = existingMovies.Movies.Count()
            };

            if (requests.Requests.Count() > 0)
            {
                var latestRequestedMovie = await _mediator.Send(new GetSingleMovieRequest { TmdbId = requests.Requests.Last().MovieId });
                var requestUser = await _userManager.FindByIdAsync(requests.Requests.Last().UserId);

                var latestRequestViewModel = new AdminPanelRquestMovieViewModel
                {
                    Movie = latestRequestedMovie.Movie,
                    User = requestUser
                };

                model.LatestRequest = latestRequestViewModel;
            }

            return View(model);
        }

        [Route("admin/requests")]
        public async Task<IActionResult> Requests()
        {
            var modelList = new List<DistinctMovieUserRequestViewModel>();
            var requests = await _mediator.Send(new GetRequestsRequest());

            foreach (var request in requests.Requests)
            {
                var movie = await _mediator.Send(new GetSingleMovieRequest { TmdbId = request.MovieId });
                var movieRequests = _context.Request.Where(x => x.MovieId == request.MovieId);

                var distinctrequest = new DistinctMovieUserRequestViewModel();
                distinctrequest.Movie = movie.Movie;

                foreach (var req in movieRequests)
                {
                    distinctrequest.Requests.Add(new MovieUserRequestViewModel()
                    {
                        User = await _userManager.FindByIdAsync(req.UserId),
                        Request = req
                    });
                }

                distinctrequest.Status = distinctrequest.Requests.First().Request.Status;
                modelList.Add(distinctrequest);
            }

            modelList = modelList.GroupBy(x => x.Movie.TmdbId).Select(y => y.First()).ToList();
            return View(modelList);
        }

        [Route("admin/users")]
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

        [Route("admin/settings")]
        public IActionResult Settings()
        {
            var model = new AdminSettingsViewModel
            {
                RadarrAPIKey = _apikeys.Radarr,
                RadarrUrl = _path.Radarr,

                TmdbApiKey = _apikeys.TMDB,
                BaseUrl = _path.BaseURL
            };

            return View(model);
        }
    }
}