using MediaRequest.Application.Queries.Movies;
using MediaRequest.Domain.Configuration;
using MediaRequest.Domain.Radarr;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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

            var result = await response.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<Movie>(result);

            var existingMovie = _context.MoviePoster.SingleOrDefault(x => x.MovieId == request.TmdbId);

            if (existingMovie != null)
            {
                json.PosterUrl = existingMovie.PosterUrl;
                json.FanartUrl = existingMovie.FanartUrl;
            } else
            {
                var tmdbmovie = await _mediator.Send(new GetMovieMediaRequest { TMDBId = request.TmdbId });
                //json.PosterUrl = $"https://image.tmdb.org/t/p/w500{tmdbResponse.Movie.poster_path}";
                //json.FanartUrl = $"https://image.tmdb.org/t/p/original{tmdbResponse.Movie.backdrop_path}";

                json.PosterUrl = tmdbmovie.Movie.Images.Where(x => x.CoverType == "poster").First().URL;
                json.FanartUrl = tmdbmovie.Movie.Images.Where(x => x.CoverType == "fanart").First().URL;
            }

            var responseObject = new GetSingleMovieResponse()
            {
                Movie = json
            };

            return responseObject;
        }
    }
}
