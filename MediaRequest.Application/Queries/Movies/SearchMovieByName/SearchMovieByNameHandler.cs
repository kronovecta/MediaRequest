using MediaRequest.Domain;
using MediaRequest.Domain.Configuration;
using MediaRequest.Domain.Radarr;
using MediaRequest.Domain.TMDB;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Queries.Movies.SearchMovieByName
{
    public class SearchMovieByNameHandler : IRequestHandler<SearchMovieByNameRequest, SearchMovieByNameResponse>
    {
        private readonly ApiKeys _apikeys;
        private readonly ServicePath _path;

        public SearchMovieByNameHandler(IOptions<ApiKeys> apikeys, IOptions<ServicePath> path)
        {
            _apikeys = apikeys.Value;
            _path = path.Value;
        }

        public async Task<SearchMovieByNameResponse> Handle(SearchMovieByNameRequest request, CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_path.Radarr}/api/movie/lookup?apikey={_apikeys.Radarr}&term={request.SearchTerm}");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                var moviesJson = JsonConvert.DeserializeObject<List<Movie>>(result);

                var existingResponse = await client.GetAsync($"{_path.Radarr}/api/movie?apikey={_apikeys.Radarr}");
                existingResponse.EnsureSuccessStatusCode();

                var existingResponseResult = await existingResponse.Content.ReadAsStringAsync();
                var existingMovies = (JsonConvert.DeserializeObject<IEnumerable<Movie>>(result).OrderByDescending(x => x.lastInfoSync));

                var responseObject = new SearchMovieByNameResponse
                {
                    Movies = moviesJson,
                    SearchResults = moviesJson.Count
                };

                foreach (var movie in moviesJson)
                {
                    var existingMovie = existingMovies.SingleOrDefault(x => x.TMDBId == movie.TMDBId);
                    if(existingMovie != null)
                    {
                        movie.Added = existingMovie.Added;
                        movie.PhysicalRelease = existingMovie.PhysicalRelease;
                        movie.Downloaded = existingMovie.Downloaded;
                        movie.Monitored = existingMovie.Monitored; 
                    }

                    //using (var tmdbclient = new HttpClient())
                    //{
                    //    var tmdb_response = await tmdbclient.GetAsync($"https://api.themoviedb.org/3/movie/{movie.TMDBId}?api_key={request.ApiKey_TMDB}");
                    //    var tmdb_string = await tmdb_response.Content.ReadAsStringAsync();
                    //    var tmdb_movie = (JsonConvert.DeserializeObject<TMDBMovie>(tmdb_string));
                    //    var poster_path = $"https://image.tmdb.org/t/p/w500{tmdb_movie.poster_path}";
                    //    var fanart_path = $"https://image.tmdb.org/t/p/original{tmdb_movie.backdrop_path}";

                    //    movie.PosterUrl = poster_path;

                    //    movie.FanartUrl = fanart_path;
                    //    movie.PosterUrl = poster_path;
                    //}
                }

                return responseObject;
            }
        }
    }
}
