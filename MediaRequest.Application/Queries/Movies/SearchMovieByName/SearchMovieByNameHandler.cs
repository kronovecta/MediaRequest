using MediaRequest.Domain;
using MediaRequest.Domain.Radarr;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Queries.Movies.SearchMovieByName
{
    public class SearchMovieByNameHandler : IRequestHandler<SearchMovieByNameRequest, SearchMovieByNameResponse>
    {
        public async Task<SearchMovieByNameResponse> Handle(SearchMovieByNameRequest request, CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"https://tiger.seedhost.eu/robert/radarr/api/movie/lookup?apikey={request.ApiKey}&term={request.SearchTerm}");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                var moviesJson = JsonConvert.DeserializeObject<List<Movie>>(result);

                var responseObject = new SearchMovieByNameResponse
                {
                    Movies = moviesJson,
                    SearchResults = moviesJson.Count
                };

                return responseObject;
            }
        }
    }
}
