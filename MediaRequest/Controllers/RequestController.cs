using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediaRequest.WebUI.Controllers
{
    public class RequestController : Controller
    {
        private readonly IMediator _mediator;

        public RequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //public async Task<IActionResult> Request(string tmdbid)
        //{

        //    var requests = await _context.Request.ToListAsync();
        //    bool valid = false;

        //    if (!requests.Any(x => x.MovieId == tmdbid)) valid = true;

        //    if (valid)
        //    {
        //        var currentUser = await _userManager.GetUserAsync(User);
        //        var request = new UserRequest() { Status = false, MovieId = tmdbid, UserId = currentUser.Id };

        //        var command = new AddRequestCommand { Request = request };

        //        await _mediator.Send(command);
        //    }

        //    return RedirectToAction("Search", "Home");
        //}
    }
}