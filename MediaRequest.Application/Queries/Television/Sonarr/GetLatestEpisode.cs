using MediaRequest.Application.Clients;
using MediaRequest.Domain.API_Responses.Shared;
using MediaRequest.Domain.API_Responses.Sonarr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Queries.Television.Sonarr
{
    public class GetLatestEpisodeRequest : IRequest<GetLatestEpisodeResponse>
    {
        public int PageSize { get; set; }
        public Order Order { get; set; }
    }

    public class GetLatestEpisodeResponse
    {
        public History History { get; set; }
    }

    public class GetLatestEpisodeHandler : IRequestHandler<GetLatestEpisodeRequest, GetLatestEpisodeResponse>
    {
        private readonly SonarrClient _sonarrClient;

        public GetLatestEpisodeHandler(SonarrClient sonarrClient)
        {
            _sonarrClient = sonarrClient;
        }

        public async Task<GetLatestEpisodeResponse> Handle(GetLatestEpisodeRequest request, CancellationToken cancellationToken)
        {
            var response = await _sonarrClient.GetSonarrResponseSingle<History>($"api/history?pageSize=${request.PageSize}&sortKey=date&sortDir=${request.Order}");

            return new GetLatestEpisodeResponse() { History = response };
        }
    }
}
