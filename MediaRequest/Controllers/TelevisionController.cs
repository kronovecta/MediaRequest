namespace MediaRequest.WebUI.Controllers
{
    using MediaRequest.Application.Queries.Find;
    using MediaRequest.Application.Queries.Movies;
    using MediaRequest.Application.Queries.Television;
    using MediaRequest.Application.Queries.Television.Sonarr;
    using MediaRequest.WebUI.ViewModels.Television;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;

    public class TelevisionController : Controller
    {
        private readonly IMediator _mediator;

        public TelevisionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("television")]
        public async Task<IActionResult> Index()
        {
            var response = await _mediator.Send(new GetAllSeriesRequest());

            return View(response.Series);
        }

        [Route("television/{slug}")]
        public async Task<IActionResult> Show(string slug)
        {

            var tvdbId = slug.Split('-').LastOrDefault();

            //var sonarrSeries = await _mediator.Send(new LookupSeriesByIdRequest() { Id = tvdbId });
            var sonarrSeries = await _mediator.Send(new GetSingleSeriesByTvdbIdRequest { TvdbId = tvdbId });
            var tmdbLookup = await _mediator.Send(new SearchByExternalIdRequest() { Id = tvdbId, Source = ExternalSource.TVDB });

            var tmdbId = tmdbLookup.Result.TvResults.FirstOrDefault()?.Id.ToString();

            var tmdbSeries = await _mediator.Send(new Application.Queries.TMDB.GetSingleSeriesRequest(tmdbId));
            var cast = await _mediator.Send(new GetCreditsRequest() { TMDBId = tmdbSeries.Series.Id.ToString(), MediaType = Domain.MediaType.Tv });

            var model = new SeriesViewModel();

            if (sonarrSeries.Series != null)
            {
                model.SonarrSeries = sonarrSeries.Series;
                model.TmdbSeries = tmdbSeries.Series;
                model.Cast = new SeriesCreditViewModel(cast.Credits.Cast);

                //if (tmdbLookup.Result.TvResults.Any())
                //{
                //    var cast = await _mediator.Send(new GetCreditsRequest() { TMDBId = tmdbSeries.Result.TvResults.FirstOrDefault()?.Id.ToString(), MediaType = Domain.MediaType.Tv });
                //    model.Cast = new SeriesCreditViewModel(cast.Credits.Cast);
                //}
            }

            return View(model);
        }

        //[Route("television/actor/{slug}")]
        //public async Task<IActionResult> Actor(string slug)
        //{
        //    var mazeId = slug.Split("-").LastOrDefault();

        //    var actor = await _mediator.Send(new GetActorRequest(mazeId));
        //    var credits = await _mediator.Send(new GetActorWithCreditsRequest(mazeId));

        //    var model = new TelevisionActorViewModel()
        //    {
        //        Actor = actor.Actor,
        //        Credits = credits.Credits
        //    };

        //    return View(model);
        //}
    }
}
