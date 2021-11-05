using MediaRequest.Application.Clients;
using MediaRequest.Domain.API_Responses.Sonarr;
using MediaRequest.Domain.Configuration;
using MediaRequest.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Queries.Television
{
    public class GetSingleEpisodeRequest : IRequest<GetSingleEpisodeResponse>, ISonarrRequest
    {
        public int Id { get; set; }
    }

    public class GetSingleEpisodeResponse : ISonarrResponse
    {
        public Episode Episode { get; set; }
    }

    public class GetSingleEpisodeHandler : IRequestHandler<GetSingleEpisodeRequest, GetSingleEpisodeResponse>
    {
        private readonly ApiKeys _apiKeys;
        private readonly ServicePath _servicePath;
        private readonly SonarrClient _sonarrClient;

        public GetSingleEpisodeHandler(IOptions<ApiKeys> apiKeys, IOptions<ServicePath> servicePath, SonarrClient sonarrClient)
        {
            _apiKeys = apiKeys.Value;
            _servicePath = servicePath.Value;
            _sonarrClient = sonarrClient;
        }

        public async Task<GetSingleEpisodeResponse> Handle(GetSingleEpisodeRequest request, CancellationToken cancellationToken)
        {
            var model = new GetSingleEpisodeResponse();
            model.Episode = await _sonarrClient.GetResponseSingle<Episode>($"api/series?apikey={_apiKeys.Sonarr}&id={request.Id}");

            return model;
        }
    }
}
