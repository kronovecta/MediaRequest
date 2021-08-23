using MediaRequest.Application.Queries.Television;
using MediaRequest.Application.Queries.Television.TvMaze;
using MediaRequest.WebUI.ViewModels.Television;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var tvdbId = slug.Split('-').LastOrDefault();

            var series = await _mediator.Send(new LookupSeriesByIdRequest() { Id = tvdbId });
            var cast = await _mediator.Send(new GetSeriesCastRequest(series.Series.TvMazeId));

            var model = new SeriesViewModel()
            {
                Series = series.Series,
                Cast = new SeriesCreditViewModel(cast.Cast)
            };

            return View(model);
        }
    }
}
