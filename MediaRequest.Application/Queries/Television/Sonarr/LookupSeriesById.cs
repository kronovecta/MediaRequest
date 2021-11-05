using MediaRequest.Application.Clients;
using MediaRequest.Domain.API_Responses.Sonarr;
using MediaRequest.Domain.Configuration;
using MediaRequest.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Queries.Television
{
    public class LookupSeriesByIdRequest : IRequest<LookupSeriesByIdResponse>, ISonarrRequest
    {
        public string Id { get; set; }
    }

    public class LookupSeriesByIdResponse : ISonarrResponse
    {
        public Series Series { get; set; }
    }

    public class LookupSeriesByIdHandler : IRequestHandler<LookupSeriesByIdRequest, LookupSeriesByIdResponse>
    {
        private readonly SonarrClient _sonarrClient;
        public LookupSeriesByIdHandler(SonarrClient sonarrClient)
        {
            _sonarrClient = sonarrClient;
        }

        public async Task<LookupSeriesByIdResponse> Handle(LookupSeriesByIdRequest request, CancellationToken cancellationToken)
        {
            var model = new LookupSeriesByIdResponse();
            var result = await _sonarrClient.GetResponseCollection<Series>($"api/series/lookup?term=tvdb:{request.Id}");

            if(result.Any())
            {
                model.Series = result.FirstOrDefault();
            } else
            {
                model.Series = null;
            }

            return model;
        }
    }
}
