using MediaRequest.Application.Queries.Movies;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.Views.Movie.Components
{
    [ViewComponent(Name = "MovieCard")]
    public class MovieCardComponent : ViewComponent
    {
        private readonly IMediator _mediator;

        public MovieCardComponent(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int maxPriority, bool isDone, string movieId)
        {
            var movie = await GetMovie(movieId);
            return View(movie);
        }

        private async Task<Domain.Radarr.Movie> GetMovie(string movieId)
        {
            var response = await _mediator.Send(new GetSingleMovieRequest { TmdbId = movieId });
            return response.Movie;
        }
    }
}
