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
        private readonly ApiKeys _apikeys;

        public GetSingleMovieHandler(IMediaDbContext context, IMediator mediator, IOptions<ApiKeys> apikeys)
        {
            _context = context;
            _mediator = mediator;
            _apikeys = apikeys.Value;
        }

        public async Task<GetSingleMovieResponse> Handle(GetSingleMovieRequest request, CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"https://tiger.seedhost.eu/robert/radarr/api/movie/lookup/tmdb?apikey={request.Keys.Radarr}&tmdbId={request.TmdbId}");
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
                    var tmdbRespons = await _mediator.Send(new GetMovieMediaRequest { ApiKey = _apikeys.TMDB, TMDBId = request.TmdbId });
                    json.PosterUrl = tmdbRespons.Movie.PosterUrl;
                    json.FanartUrl = tmdbRespons.Movie.FanartUrl;
                }

                var responseObject = new GetSingleMovieResponse()
                {
                    Movie = json
                };

                return responseObject;
            }
        }
    }
}
