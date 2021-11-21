using MediaRequest.Application.Clients;
using MediaRequest.Application.Parsers;
using MediaRequest.Domain.API_Responses.Sonarr;
using MediaRequest.Domain.API_Responses.TMDB;
using MediaRequest.Domain.Configuration;
using MediaRequest.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Queries.TMDB
{
    public class GetCombinedCreditsRequest : IRequest<GetCombinedCreditsResponse>, IApolloType
    {
        public GetCombinedCreditsRequest(string actorId)
        {
            ActorId = actorId;
        }

        public string ActorId { get; set; }
    }

    public class GetCombinedCreditsResponse : IApolloType
    {
        public CombinedCredits Credits { get; set; }
    }

    public class GetCombinedCreditsHandler : IRequestHandler<GetCombinedCreditsRequest, GetCombinedCreditsResponse>
    {
        private readonly TMDBClient _tmdbClient;

        public GetCombinedCreditsHandler(TMDBClient tmdbClient)
        {
            _tmdbClient = tmdbClient;
        }

        public async Task<GetCombinedCreditsResponse> Handle(GetCombinedCreditsRequest request, CancellationToken cancellationToken)
        {
            var response = await _tmdbClient.GetSingle<CombinedCredits>($"person/{request.ActorId}/combined_credits");
            var model = new GetCombinedCreditsResponse()
            {
                Credits = response
            };

            return model;
        }
    }
}
