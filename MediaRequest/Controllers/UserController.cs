using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediaRequest.Application;
using MediaRequest.Application.Queries.Movies;
using MediaRequest.Application.Queries.Movies.GetSingleExistingMovie;
using MediaRequest.Application.Queries.Movies.GetUpcoming;
using MediaRequest.Data;
using MediaRequest.WebUI.Exceptions;
using MediaRequest.WebUI.Models;
using MediaRequest.WebUI.Models.IdentityModels;
using MediaRequest.WebUI.ViewModels.Account;
using MediaRequest.WebUI.ViewModels.Profile;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MediaRequest.WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IdentityContext _identityContext;
        private readonly IMediaDbContext _mediaContext;

        public UserController(IMediator mediator, UserManager<ApplicationUser> userManager, IdentityContext identityContext, IMediaDbContext mediaContext)
        {
            _mediator = mediator;
            _userManager = userManager;
            _identityContext = identityContext;
            _mediaContext = mediaContext;
        }

        [Route("Profile")]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = new ProfileViewModel()
            {
                User = await _userManager.GetUserAsync(User),
                Requests = new List<RequestViewModel>()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadAvatar(IFormFile image)
        {
            var maxLength = 5000000;
            var allowedTypes = new string[3] { "jpg", "jpeg", "png" };

            if (ModelState.IsValid)
            {
                if (!allowedTypes.Contains(image.FileName.Split('.')[1]))
                {
                    throw new FileTypeNotAllowedException("Filetype is not allowed");
                }

                if (image.Length > maxLength)
                {
                    throw new FilesizeTooLarge("Only files up to 5MB allowed"); 
                }

                var user = await _userManager.GetUserAsync(User);
                byte[] byteimg = null;
                using (var filestream = image.OpenReadStream())
                {
                    using(var memorystream = new MemoryStream())
                    {
                        filestream.CopyTo(memorystream);
                        byteimg = memorystream.ToArray();
                    }
                }

                var contextUser = _identityContext.AspNetUsers.SingleOrDefault(x => x.Id == user.Id);
                if(contextUser != null)
                {
                    contextUser.Avatar = byteimg;
                    await _identityContext.SaveChangesAsync();
                }
            }
            
            return RedirectToAction("Profile", "User");
        }

        [Route("Profile/UpdatePassword")]
        public IActionResult UpdatePassword()
        {
            return View();
        }

        [HttpPost]
        [Route("Profile/UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.NewPassword == model.ConfirmPassword)
                {
                    var result = await _userManager.ChangePasswordAsync(await _userManager.GetUserAsync(User), model.CurrentPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        TempData["success"] = "Password updated!";
                        return RedirectToAction("Profile");
                    } else
                    {
                        TempData["error"] = $"Error: ";
                        foreach (var error in result.Errors)
                        {
                            TempData["error"] += $"{error.Description}. ";
                        }
                        return View();
                    }
                } else
                {
                    TempData["error"] = $"Passwords do not match";
                    return RedirectToAction("UpdatePassword");
                }
            }

            return RedirectToAction("Profile");
        }

        [Route("Profile/Calendar")]
        public async Task<IActionResult> Upcoming()
        {
            var result = await _mediator.Send(new GetUpcomingRequest { Days = 365 });
            var model = new CalendarViewModel() { Events = result.Movies };

            return View(model);
        }

        [Route("Profile/Requests")]
        public async Task<IActionResult> Requests()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = new List<RequestViewModel>();

            var requests = _mediaContext.Request.Where(x => x.UserId == user.Id).ToList();
            foreach (var request in requests)
            {
                var result = await _mediator.Send(new GetSingleMovieRequest() { TmdbId = request.MovieId });
                model.Add(new RequestViewModel { Movie = result.Movie, Request = request });
            }

            return View(model);
        }
    }
}