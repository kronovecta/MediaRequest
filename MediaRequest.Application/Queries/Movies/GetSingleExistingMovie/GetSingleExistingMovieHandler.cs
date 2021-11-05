using MediaRequest.Domain.Configuration;
using MediaRequest.Domain.API_Responses.Radarr.v3;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediaRequest.Application.Clients;

namespace MediaRequest.Application.Queries.Movies.GetSingleExistingMovie
{
    public class GetSingleExistingMovieHandler : IRequestHandler<GetSingleExistingMovieRequest, GetSingleExistingMovieResponse>
    {
        private readonly RadarrClient _radarrClient;

        public GetSingleExistingMovieHandler(RadarrClient radarrClient)
        {
            _radarrClient = radarrClient;
        }

        public async Task<GetSingleExistingMovieResponse> Handle(GetSingleExistingMovieRequest request, CancellationToken cancellationToken)
        {
            var res = await _radarrClient.GetResponseSingle<Movie>($"api/v3/movie/{request.RadarrMovieId}");

            var response = new GetSingleExistingMovieResponse
            {
                Movie = res
            };

            return response;
        }
    }
}
