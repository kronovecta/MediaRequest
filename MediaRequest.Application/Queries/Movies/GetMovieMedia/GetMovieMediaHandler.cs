using MediaRequest.Domain.TMDB;
using MediatR;
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
        public async Task<GetMovieMediaResponse> Handle(GetMovieMediaRequest request, CancellationToken cancellationToken)
        {
            // https://api.themoviedb.org/3/movie/109445?api_key=

            using (var client = new HttpClient())
            {
                var result = await client.GetAsync($"https://api.themoviedb.org/3/movie/{request.TMDBId}?api_key={request.ApiKey}");
                result.EnsureSuccessStatusCode();

                var resultString = await result.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<TMDBMovie>(resultString);

                var response = new GetMovieMediaResponse() { Movie = json };

                return response;
            }
        }
    }
}
