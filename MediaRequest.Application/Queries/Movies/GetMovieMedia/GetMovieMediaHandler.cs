using MediaRequest.Domain.Configuration;
using MediaRequest.Domain.Radarr;
using MediaRequest.Domain.TMDB;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Queries.Movies
{
    public class GetMovieMediaHandler : IRequestHandler<GetMovieMediaRequest, GetMovieMediaResponse>
    {
        private readonly IHttpHelper _http;

        public GetMovieMediaHandler(IHttpHelper http)
        {
            _http = http;
        }

        public async Task<GetMovieMediaResponse> Handle(GetMovieMediaRequest request, CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            {
                var result = await _http.GetMovie(request);
                result.EnsureSuccessStatusCode();

                var resultString = await result.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<Movie>(resultString);

                var response = new GetMovieMediaResponse() { Movie = json };

                return response;
            }
        }
    }
}
