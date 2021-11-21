namespace MediaRequest.Application.Queries.Television.Sonarr
{
    using MediaRequest.Application.Clients;
    using MediaRequest.Domain.API_Responses.Sonarr;
    using MediaRequest.Domain.Interfaces;
    using MediatR;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetSingleSeriesByTvdbIdRequest : IRequest<GetSingleSeriesByTvdbIdResponse>
    {
        public string TvdbId { get; set; }
    }

    public class GetSingleSeriesByTvdbIdResponse : ISonarrResponse
    {
        public Series Series { get; set; }
    }

    public class GetSingleSeriesByTvdbIdHandler : IRequestHandler<GetSingleSeriesByTvdbIdRequest, GetSingleSeriesByTvdbIdResponse>
    {
        private readonly SonarrClient _sonarrClient;
        public GetSingleSeriesByTvdbIdHandler(SonarrClient sonarrClient)
        {
            _sonarrClient = sonarrClient;
        }

        public async Task<GetSingleSeriesByTvdbIdResponse> Handle(GetSingleSeriesByTvdbIdRequest request, CancellationToken cancellationToken)
        {
            if(int.TryParse(request.TvdbId, out int tvdbId)) {
                var model = new GetSingleSeriesByTvdbIdResponse();
                var result = await _sonarrClient.GetResponseCollection<Series>($"api/series");

                if (result.Any())
                {
                    model.Series = result.FirstOrDefault(x => x.TvdbId == tvdbId);
                }
                else
                {
                    model.Series = null;
                }

                return model;
            }

            throw new Exception("Unable to parse tvdbId");
        }
    }
}
