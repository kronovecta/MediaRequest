using MediaRequest.Application.Parsers;
using MediaRequest.Application.Queries.Movies;
using MediaRequest.Domain.Configuration;
using MediaRequest.Domain.API_Responses.Radarr.v3;
using MediatR;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediaRequest.Application.Clients;

namespace MediaRequest.Application.Queries
{
    public class GetSingleMovieHandler : IRequestHandler<GetSingleMovieRequest, GetSingleMovieResponse>
    {
        private readonly RadarrClient _radarrClient;

        public GetSingleMovieHandler(RadarrClient radarrClient)
        {
            _radarrClient = radarrClient;

        }

        public async Task<GetSingleMovieResponse> Handle(GetSingleMovieRequest request, CancellationToken cancellationToken)
        {
            var res = await _radarrClient.GetResponseSingle<Movie>($"api/v3/movie/lookup/tmdb?tmdbId={request.TmdbId}");
            var model = new GetSingleMovieResponse() { Movie = res };

            return model;
        }
    }
}
