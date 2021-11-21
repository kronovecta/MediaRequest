using MediaRequest.Application.Clients;
using MediaRequest.Domain.API_Responses.Radarr.v3;
using MediaRequest.Domain.API_Responses.Shared;
using MediaRequest.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Queries.Movies.GetHistory
{
    public class GetHistoryRequest : IRequest<GetHistoryResponse>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public Order Order { get; set; }
    }

    public class GetHistoryResponse : IRequestResponse
    {
        public History History { get; set; }
    }

    public class GetHistoryHandler : IRequestHandler<GetHistoryRequest, GetHistoryResponse>
    {
        private readonly RadarrClient _radarrClient;

        public GetHistoryHandler(RadarrClient radarrClient)
        {
            _radarrClient = radarrClient;
        }

        public async Task<GetHistoryResponse> Handle(GetHistoryRequest request, CancellationToken cancellationToken)
        {
            var res = await _radarrClient.GetResponseSingle<History>($"api/v3/history?page={request.Page}&sortDirection={request.Order}&sortKey=date");

            return new GetHistoryResponse { History = res };
        }
    }
}
