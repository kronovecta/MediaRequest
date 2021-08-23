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
    public class LookupSeriesByTermRequest : IRequest<LookupSeriesByTermResponse>, ISonarrRequest
    {
        public string Term { get; set; }
    }

    public class LookupSeriesByTermResponse : ISonarrResponse
    {
        public IEnumerable<Series> Series { get; set; }
    }

    public class LookupSeriesByTermHandler : IRequestHandler<LookupSeriesByTermRequest, LookupSeriesByTermResponse>
    {
        private readonly SonarrClient _sonarrClient;
        public LookupSeriesByTermHandler(SonarrClient sonarrClient)
        {
            _sonarrClient = sonarrClient;
        }

        public async Task<LookupSeriesByTermResponse> Handle(LookupSeriesByTermRequest request, CancellationToken cancellationToken)
        {
            var model = new LookupSeriesByTermResponse();
            model.Series = await _sonarrClient.GetSonarrResponseCollection<Series>($"api/series/lookup?term={request.Term}");

            return model;
        }
    }
}
