using MediaRequest.Application.Queries.Movies;
using MediaRequest.WebUI.ViewModels;
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

        public async Task<IViewComponentResult> InvokeAsync(int maxPriority, bool isDone, string movieId = "", string tmdbId = "")
        {
            var movie = await GetMovie(movieId);

            return View(movie);
        }

        private async Task<Domain.Radarr.Movie> GetMovie(string movieId)
        {
            var response = await _mediator.Send(new GetSingleMovieRequest { TmdbId = movieId });
            response.Movie.FanartUrl = response.Movie.FanartUrl != "http://image.tmdb.org/t/p/original" ? response.Movie.FanartUrl : "https://www.themoviedb.org/assets/2/v4/glyphicons/basic/glyphicons-basic-38-picture-grey-c2ebdbb057f2a7614185931650f8cee23fa137b93812ccb132b9df511df1cfac.svg";
            return response.Movie;
        }
    }
}
