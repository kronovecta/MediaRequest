using MediaRequest.Application.Parsers;
using MediaRequest.Application.Queries.Movies;
using MediaRequest.Domain.Configuration;
using MediaRequest.Domain.Radarr;
using MediatR;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Queries
{
    public class GetSingleMovieHandler : IRequestHandler<GetSingleMovieRequest, GetSingleMovieResponse>
    {
        private readonly IMediaDbContext _context;
        private readonly IMediator _mediator;
        private readonly IHttpHelper _http;

        public GetSingleMovieHandler(IHttpHelper http, IMediaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
            _http = http;
        }

        public async Task<GetSingleMovieResponse> Handle(GetSingleMovieRequest request, CancellationToken cancellationToken)
        {
            var response = await _http.GetMovie(request);
            response.EnsureSuccessStatusCode();

            var model = new GetSingleMovieResponse();

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                var json = await JsonSerializer.DeserializeAsync<Movie>(stream, DefaultJsonSettings.Settings);
                model.Movie = json;
            }

            var existingMovie = _context.MoviePoster.SingleOrDefault(x => x.MovieId == request.TmdbId);

            if (existingMovie != null)
            {
                model.Movie.PosterUrl = existingMovie.PosterUrl;
                model.Movie.FanartUrl = existingMovie.FanartUrl;
            } else
            {
                var tmdbmovie = await _mediator.Send(new GetMovieMediaRequest { TMDBId = request.TmdbId });

                model.Movie.PosterUrl = tmdbmovie.Movie.Images.Where(x => x.CoverType == "poster").First().URL;
                model.Movie.FanartUrl = tmdbmovie.Movie.Images.Where(x => x.CoverType == "fanart").First().URL;
            }

            return model;
        }
    }
}
