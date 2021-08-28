using MediaRequest.Application.Queries.Television;
using MediaRequest.Application.Queries.Television.TvMaze;
using MediaRequest.WebUI.ViewModels.Television;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using MediaRequest.Application.Queries.Find;
using System.Threading.Tasks;
using MediaRequest.Application.Queries.Movies;

namespace MediaRequest.WebUI.Controllers
{
    public class TelevisionController : Controller
    {
        private readonly IMediator _mediator;

        public TelevisionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _mediator.Send(new GetAllSeriesRequest());

            return View(response.Series);
        }

        [Route("television/{slug}")]
        public async Task<IActionResult> Series(string slug)
        {
            var model = new SeriesViewModel();

            var tvdbId = slug.Split('-').LastOrDefault();

            var sonarrSeries = await _mediator.Send(new LookupSeriesByIdRequest() { Id = tvdbId });
            var tmdbSeries = await _mediator.Send(new SearchByExternalIdRequest() { Id = tvdbId, Source = ExternalSource.TVDB });
            
            if(sonarrSeries.Series != null)
            {
                model.Series = sonarrSeries.Series;

                if (tmdbSeries.Result.TvResults.Any())
                {
                    var cast = await _mediator.Send(new GetCreditsRequest() { TMDBId = tmdbSeries.Result.TvResults.FirstOrDefault()?.Id.ToString(), MediaType = Domain.MediaType.Tv });
                    model.Cast = new SeriesCreditViewModel(cast.Credits.Cast);
                }
            }

            return View(model);
        }

        [Route("television/actor/{slug}")]
        public async Task<IActionResult> Actor(string slug)
        {
            var mazeId = slug.Split("-").LastOrDefault();

            var actor = await _mediator.Send(new GetActorRequest(mazeId));
            var credits = await _mediator.Send(new GetActorWithCreditsRequest(mazeId));

            var model = new TelevisionActorViewModel()
            {
                Actor = actor.Actor,
                Credits = credits.Credits
            };

            return View(model);
        }
    }
}
