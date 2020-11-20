using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaRequest.Application;
using MediaRequest.Application.Commands;
using MediaRequest.Application.Commands.ApproveRequest;
using MediaRequest.Application.Commands.CancelRequest;
using MediaRequest.Application.Queries.Movies;
using MediaRequest.Domain;
using MediaRequest.Domain.Radarr;
using MediaRequest.WebUI.Models.IdentityModels;
using MediaRequest.WebUI.ViewModels.SingleMovie;
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
            var currentUser = await _userManager.GetUserAsync(User);
            
            var movie = await _mediator.Send(new GetSingleMovieRequest { TmdbId = tmdbid });

            if (await _context.Request.Where(x => x.UserId == currentUser.Id && x.MovieId == tmdbid).CountAsync() > 0)
            {
                return RedirectToAction("ShowMovie", "Movie", new { slug = movie.Movie.TitleSlug });
            }
                
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
            return RedirectToAction("ShowMovie", "Movie", new { slug = movie.Movie.TitleSlug });
        }

        public async Task<IActionResult> ApproveRequest(string id)
        {
            //var userRequest = await _context.Request.SingleOrDefaultAsync(x => x.Id == requestId);
            var requests = _context.Request.Where(x => x.MovieId == id);

            var movie = await _mediator.Send(new GetSingleMovieRequest() { TmdbId = id });
            var request = GenerateApproveRequest(movie.Movie);

            var command = new ApproveRequestCommand { RequestObject = request };
            var result = await _mediator.Send(command);

            if (result == true)
            {
                //userRequest.Status = true;
                await requests.ForEachAsync(x => x.Status = true);
                await _context.SaveChangesAsync();
            }
            else
            {
                //userRequest.Status = false;
                await requests.ForEachAsync(x => x.Status = false);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Requests", "Admin");
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

        [HttpGet]
        public IActionResult AddMovie()
        {
            HttpContext.Response.StatusCode = 405;
            return RedirectToAction("Error", "Home");
        }

        /// <summary>
        ///     Lets admins add movies directly, bypassing the need for requests
        /// </summary>
        /// <param name="tmdb"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddMovie(string tmdbid)
        {
            var movie = await _mediator.Send(new GetSingleMovieRequest() { TmdbId = tmdbid });
            var request = GenerateApproveRequest(movie.Movie);


            var command = new ApproveRequestCommand { RequestObject = request };
            var result = await _mediator.Send(command);

            var model = new MovieViewModel { Movie = movie.Movie, Accepted = result, Requested = result };

            if (result)
                TempData["Success"] = "Movie added";
            else
                TempData["Error"] = "There was an error adding the movie";

            return PartialView("_RequestMovieButton", model);
        }

        private MovieRequestObject GenerateApproveRequest(Movie movie)
        {
            var request = new MovieRequestObject()
            {
                title = movie.Title,
                qualityProfileId = 7,
                images = movie.Images,
                tmdbId = movie.TMDBId.ToString(),
                titleSlug = movie.TitleSlug,
                ProfileId = 4,
                year = movie.Year,
                path = $"/home34/robert/downloads/movies/{movie.Title} ({movie.Year})".Replace(":", ""),
                addOptions = new MovieRequestObjectOptions
                {
                    searchForMovie = "true"
                }
            };

            return request;
        }
    }
}