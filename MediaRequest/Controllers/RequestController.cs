using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaRequest.Application;
using MediaRequest.Application.Commands;
using MediaRequest.Application.Commands.CancelRequest;
using MediaRequest.Application.Queries.Movies;
using MediaRequest.Domain;
using MediaRequest.WebUI.Models.IdentityModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediaRequest.WebUI.Controllers
{
    public class RequestController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMediaDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RequestController(IMediator mediator, IMediaDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<IActionResult> AddRequest(string tmdbid)
        {
            var requests = await _context.Request.ToListAsync();
            bool valid = false;

            if (!requests.Any(x => x.MovieId == tmdbid)) valid = true;

            if (valid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                
                var command = new AddRequestCommand
                {
                    Request = new UserRequest()
                    {
                        Status = false,
                        MovieId = tmdbid,
                        UserId = currentUser.Id
                    }
                };

                await _mediator.Send(command);
            }

            var movie = await _mediator.Send(new GetSingleMovieRequest { TmdbId = tmdbid });
            var movieslug = movie.Movie.TitleSlug;

            return RedirectToAction("ShowMovie", "Movie", new { slug = movieslug });
        }

        public async Task<IActionResult> CancelRequest(int requestid)
        {
            if(ModelState.IsValid)
            {
                var command = new CancelRequestCommand { RequestID = requestid };
                try
                {
                    var response = await _mediator.Send(command);
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                    return RedirectToAction("Profile", "User");
                }

                TempData["Success"] = "Request succesfully removed";
                return RedirectToAction("Profile", "User");
            } else
            {
                TempData["Error"] = "Invalid operation";
                return RedirectToAction("Profile", "User");
            }
        }
    }
}