using MediaRequest.Application.Clients;
using MediaRequest.Application.Parsers;
using MediaRequest.Domain.API_Responses.Sonarr;
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

namespace MediaRequest.Application.Queries.Television
{
    public class GetAllSeriesRequest : IRequest<GetAllSeriesResponse>, ISonarrRequest
    {
    }

    public class GetAllSeriesResponse : ISonarrResponse
    {
        public GetAllSeriesResponse()
        {
            Series = new List<Series>();
        }

        public IEnumerable<Series> Series { get; set; }
    }

    public class GetAllSeriesHandler : IRequestHandler<GetAllSeriesRequest, GetAllSeriesResponse>
    {
        private readonly ServicePath _servicePath;
        private readonly ApiKeys _apiKeys;
        private readonly SonarrClient _sonarrClient;

        public GetAllSeriesHandler(IOptions<ApiKeys> apiKeys, IOptions<ServicePath> servicePath, SonarrClient sonarrClient)
        {
            _apiKeys = apiKeys.Value;
            _servicePath = servicePath.Value;
            _sonarrClient = sonarrClient;
        }

        public async Task<GetAllSeriesResponse> Handle(GetAllSeriesRequest request, CancellationToken cancellationToken)
        {
            var model = new GetAllSeriesResponse();

            model.Series = await _sonarrClient.GetSonarrResponseCollection<Series>($"api/series");

            return model;
        }
    }
}
