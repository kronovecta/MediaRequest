using MediaRequest.Domain;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Queries
{
    public class GetSingleMovieHandler : IRequestHandler<GetSingleMovieRequest, GetSingleMovieResponse>
    {
        public async Task<GetSingleMovieResponse> Handle(GetSingleMovieRequest request, CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"https://tiger.seedhost.eu/robert/radarr/api/movie/lookup/tmdb?apikey=<API_KEY>&tmdbId={request.TmdbId}");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<Movie>(result);

                var responseObject = new GetSingleMovieResponse()
                {
                    Movie = json
                };

                return responseObject;
            }
        }
    }
}
