using MediaRequest.Application.Clients;
using MediaRequest.Domain.API_Responses.TvMaze;
using MediaRequest.Domain.Configuration;
using MediaRequest.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Queries.Television.TvMaze
{
    public class GetSeriesCastRequest : IRequest<GetSeriesCastResponse>, IApolloType
    {
        public int TvMazeId { get; set; }
        public GetSeriesCastRequest(int tvMazeId)
        {
            TvMazeId = tvMazeId;
        }
    }

    public class GetSeriesCastResponse : IApolloType
    {
        public IEnumerable<CastMember> Cast { get; set; }
    }

    public class GetSeriesCastHandler : IRequestHandler<GetSeriesCastRequest, GetSeriesCastResponse>
    {
        private readonly ApiKeys _apiKeys;
        private readonly ServicePath _servicePath;
        private readonly TVMazeClient _tvMazeClient;

        public GetSeriesCastHandler(IOptions<ApiKeys> apiKeys, IOptions<ServicePath> servicePath, TVMazeClient tvMazeClient)
        {
            _apiKeys = apiKeys.Value;
            _servicePath = servicePath.Value;
            _tvMazeClient = tvMazeClient;
        }

        public async Task<GetSeriesCastResponse> Handle(GetSeriesCastRequest request, CancellationToken cancellationToken)
        {
            var model = new GetSeriesCastResponse();
            model.Cast = await _tvMazeClient.GetCollection<CastMember>($"shows/{request.TvMazeId}/cast");

            return model;
        }
    }
}
