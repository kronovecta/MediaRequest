using MediaRequest.Application.Queries.People.GetCombinedMedia;
using MediaRequest.Application.Queries.Television.TvMaze;
using MediaRequest.Application.Queries.TMDB;
using MediaRequest.WebUI.ViewModels;
using MediaRequest.WebUI.ViewModels.Television;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.Controllers
{
    public class ActorController : Controller
    {
        private readonly IMediator _mediator;

        public ActorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("actor/{slug}")]
        public async Task<IActionResult> Actor(string slug)
        {
            var actorId = slug.Split("-").Last();

            var response = await _mediator.Send(new GetCombinedMediaRequest { ActorID = actorId });
            //var movies = await _mediator.Send(new GetPopularMoviesRequest { ActorId = actorId });

            //var m = movies.Movies.ToList().GetRange(0, (movies.Movies.Count() > 9 ? 9 : movies.Movies.Count())).Select(x => new Movie
            //{
            //    Id = x.Id,
            //    Title = x.Title,
            //    FanartUrl = x.BackdropPath ?? "https://www.themoviedb.org/assets/2/v4/glyphicons/basic/glyphicons-basic-38-picture-grey-c2ebdbb057f2a7614185931650f8cee23fa137b93812ccb132b9df511df1cfac.svg",
            //    Overview = x.Overview
            //});

            var previousWorks = await _mediator.Send(new GetCombinedCreditsRequest(actorId));

            var model = new ActorViewModel
            {
                Actor = response.Actor,
                PreviousWorks = new CombinedCreditsViewModel
                {
                    Cast = previousWorks.Credits.Cast,
                    Crew = previousWorks.Credits.Crew
                }
            };

            return View(model);
        }
    }
}
