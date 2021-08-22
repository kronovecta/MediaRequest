using MediaRequest.Application.Queries.Television;
using MediaRequest.WebUI.ViewModels.Series;
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
            var tvdbId = slug.Split('-').FirstOrDefault();

            var response = await _mediator.Send(new LookupSeriesByIdRequest() { Id = tvdbId });

            var model = new SeriesViewModel()
            {
                Series = response.Series
            };

            return View(model);
        }
    }
}
