using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaRequest.Application;
using MediaRequest.Application.Commands;
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

        public async Task<IActionResult> Request(string tmdbid)
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

            return Redirect(HttpContext.Request.Path);
        }
    }
}