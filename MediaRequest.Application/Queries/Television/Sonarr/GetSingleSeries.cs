using MediaRequest.Application.Clients;
using MediaRequest.Domain.API_Responses.Sonarr;
using MediaRequest.Domain.Configuration;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediaRequest.Domain.Interfaces;
using MediaRequest.Domain;

namespace MediaRequest.Application.Queries.Television
{
    public class GetSingleSeriesRequest : IRequest<GetSingleSeriesResponse>, ISonarrRequest
    {
        public int Id { get; set; }

        public GetSingleSeriesRequest(int id)
        {
            Id = id;
        }
    }

    public class GetSingleSeriesResponse : ISonarrResponse
    {
        public Series TvShow { get; set; }
    }

    public class GetSingleSeriesHandler : IRequestHandler<GetSingleSeriesRequest, GetSingleSeriesResponse>
    {
        private readonly SonarrClient _sonarrClient;

        public GetSingleSeriesHandler(SonarrClient sonarrClient)
        {
            _sonarrClient = sonarrClient;
        }

        public async Task<GetSingleSeriesResponse> Handle(GetSingleSeriesRequest request, CancellationToken cancellationToken)
        {
            var model = new GetSingleSeriesResponse();
            model.TvShow = await _sonarrClient.GetResponseSingle<Series>($"api/series/{request.Id}");
            
            return model;
        }
    }
}
