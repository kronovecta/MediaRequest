using MediaRequest.Application.Clients;
using MediaRequest.Domain.API_Responses.TMDB;
using MediaRequest.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Queries.TMDB
{
    public class GetSingleSeriesRequest : IRequest<GetSingleSeriesResponse>, IApolloType
    {
        public string Id { get; set; }
     
        public GetSingleSeriesRequest(string id)
        {
            Id = id;
        }
    }

    public class GetSingleSeriesResponse : IApolloType
    {
        public Series Series { get; set; }
    }

    public class GetSingleSeriesHandler : IRequestHandler<GetSingleSeriesRequest, GetSingleSeriesResponse>
    {
        private readonly TMDBClient _tmdbClient;

        public GetSingleSeriesHandler(TMDBClient tmdbClient)
        {
            _tmdbClient = tmdbClient;
        }

        public async Task<GetSingleSeriesResponse> Handle(GetSingleSeriesRequest request, CancellationToken cancellationToken)
        {
            var model = new GetSingleSeriesResponse();
            model.Series = await _tmdbClient.GetSingle<Series>($"tv/{request.Id}");

            return model;
        }
    }
}
