using MediaRequest.Application.Parsers;
using MediaRequest.Domain.API_Responses.TMDB;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Queries.Movies.GetTMDBContent
{
    public class GetTMDBMediaRequest : IRequest<GetTMDBMediaResponse>
    {
        public GetTMDBMediaRequest(int tmdbId)
        {
            TMDB = tmdbId;
        }

        public int TMDB { get; set; }
    }

    public class GetTMDBMediaHandler : IRequestHandler<GetTMDBMediaRequest, GetTMDBMediaResponse>
    {
        private readonly IHttpHelper _http;

        public GetTMDBMediaHandler(IHttpHelper http)
        {
            _http = http;
        }

        public async Task<GetTMDBMediaResponse> Handle(GetTMDBMediaRequest request, CancellationToken cancellationToken)
        {
            var response = await _http.GetTMDBMedia(request);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var model = new GetTMDBMediaResponse();

            using (var stream = await response.Content.ReadAsStreamAsync()) 
            {
                var json = await JsonSerializer.DeserializeAsync<Images>(stream, DefaultJsonSettings.Settings);
                model.Backdrops = json.Backdrops;
                model.Posters = json.Posters;
            }

            return model;
        }
    }

    public class GetTMDBMediaResponse
    {
        public List<Poster> Posters { get; set; }
        public List<Backdrop> Backdrops { get; set; }
    }
}
