using MediaRequest.Domain;
using MediaRequest.Domain.Configuration;
using MediaRequest.Domain.Radarr;
using MediaRequest.Domain.TMDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Queries.Movies
{
    static class Functions
    {
        public static int GetTotalPages(List<Movie> movies, int results)
        {
            var amount = 0;
            if(movies.Count() % results != 0)
            {
                amount = (movies.Count() / results) + 1;
            } else
            {
                amount = movies.Count() / results;
            }
            
            return amount;
        }
    }

    public class GetExistingMoviesHandler : IRequestHandler<GetExistingMoviesRequest, GetExistingMoviesResponse>
    {
        private readonly IMediaDbContext _context;
        private readonly ApiKeys _keys;
        private readonly ServicePath _path;
        private readonly IHttpHelper _http;

        public GetExistingMoviesHandler(IHttpHelper http, IMediaDbContext context, IOptions<ServicePath> path, IOptions<ApiKeys> keys)
        {
            _context = context;
            _keys = keys.Value;
            _path = path.Value;
            _http = http;
        }

        public async Task<GetExistingMoviesResponse> Handle(GetExistingMoviesRequest request, CancellationToken cancellationToken)
        {
            var results = request.Amount;

            using (var client = new HttpClient())
            {
                var res = await _http.GetMovie();
                res.EnsureSuccessStatusCode();

                var result = await res.Content.ReadAsStringAsync();
                var movies = JsonConvert.DeserializeObject<IEnumerable<Movie>>(result).Reverse().ToList();

                var totalPages = 0;

                if(request.Amount > 0)
                {
                    totalPages = Functions.GetTotalPages(movies, request.Amount);
                    movies = movies.Skip(request.Amount * (request.CurrentPage - 1)).Take(request.Amount).ToList();
                }
                
                //movies = movies.GetRange((results * request.CurrentPage), results);

                var moviePosters = await _context.MoviePoster.ToListAsync<MoviePoster>();

                foreach (var movie in movies)
                {
                    if (moviePosters.Any(x => x.MovieId == movie.TMDBId))
                    {
                        movie.PosterUrl = _path.Radarr + movie.Images.SingleOrDefault(x => x.CoverType == "poster").URL.Split(new string[] { "/radarr" }, StringSplitOptions.None)[1];
                        movie.FanartUrl = _path.Radarr + movie.Images.SingleOrDefault(x => x.CoverType == "fanart").URL.Split(new string[] { "/radarr" }, StringSplitOptions.None)[1];
                    }
                    else
                    {
                        using (var tmdbclient = new HttpClient())
                        {
                            var tmdb_response = await tmdbclient.GetAsync(_path.TMDB +  $"/movie/{movie.TMDBId}?api_key={_keys.TMDB}");
                            var tmdb_string = await tmdb_response.Content.ReadAsStringAsync();
                            var tmdb_movie = (JsonConvert.DeserializeObject<TMDBMovie>(tmdb_string));

                            var poster_path = "https://image.tmdb.org/t/p/w500" + tmdb_movie.poster_path;
                            var fanart_path = "https://image.tmdb.org/t/p/original" + tmdb_movie.backdrop_path;

                            movie.PosterUrl = poster_path;

                            var moviePoster = new MoviePoster()
                            {
                                MovieId = movie.TMDBId,
                                PosterUrl = poster_path,
                                FanartUrl = fanart_path
                            };

                            await _context.MoviePoster.AddAsync(moviePoster);
                            await _context.SaveChangesAsync();



                            movie.FanartUrl = fanart_path;
                            movie.PosterUrl = poster_path;
                        }
                    }
                }

                var latestMovie = movies.Where(x => x.Downloaded == true).First();

                var response = new GetExistingMoviesResponse
                {
                    Movies = movies,
                    LatestMovie = latestMovie,
                    TotalPages = totalPages,
                    CurrentPage = request.CurrentPage
                };

                return response;
            }
        }
    }

    public class GetExistingMoviesFilteredHandler : IRequestHandler<GetExistingMoviesFilteredRequest, GetExistingMoviesResponse>
    {
        private readonly IMediaDbContext _context;
        private readonly ServicePath _path;
        private readonly IHttpHelper _http;

        public GetExistingMoviesFilteredHandler(IHttpHelper http, IMediaDbContext context, IOptions<ServicePath> path)
        {
            _path = path.Value;
            _context = context;
            _http = http;
        }

        public async Task<GetExistingMoviesResponse> Handle(GetExistingMoviesFilteredRequest request, CancellationToken cancellationToken)
        {
            var results = 10;

            using (var client = new HttpClient())
            {
                var res = await _http.GetMovie();
                res.EnsureSuccessStatusCode();
                var result = await res.Content.ReadAsStringAsync();
                var movies = JsonConvert.DeserializeObject<IEnumerable<Movie>>(result).ToList();
                var latestMovie = movies.Where(x => x.Downloaded == true).First();

                //var totalPages = Functions.GetTotalPages(movies, results);
                //movies = movies.GetRange((results * request.CurrentPage), results);

                if(request.Input != null)
                {
                    movies = movies.Where(x => x.Title.ToLower().Contains(request.Input.ToLower()) || x.AlternativeTitles.Any(y => y.title.ToLower().Contains(request.Input.ToLower())) || x.Studio.ToLower().Contains(request.Input.ToLower())).ToList();
                }

                if (request.FilterMode == 1)
                {
                    movies = movies.Where(x => x.Downloaded == true).Reverse().ToList();
                }

                else if (request.FilterMode == 2)
                {
                    movies = movies.Where(x => x.Downloaded == false).Reverse().ToList();
                }

                var totalPages = Functions.GetTotalPages(movies, results);
                movies = movies.Skip(results * request.CurrentPage-1).Take(results).ToList();
                //movies = movies.GetRange((results * request.CurrentPage), results);


                if (movies.Count() > 0)
                {
                    var moviePosters = _context.MoviePoster.Where(x => movies.Any(y => y.TMDBId == x.MovieId)).ToList();

                    foreach (var movie in movies)
                    {
                        movie.PosterUrl = _path.Radarr + movie.Images.SingleOrDefault(x => x.CoverType == "poster").URL.Split(new string[] { "/radarr" }, StringSplitOptions.None)[1];
                        movie.FanartUrl = _path.Radarr + movie.Images.SingleOrDefault(x => x.CoverType == "fanart").URL.Split(new string[] { "/radarr" }, StringSplitOptions.None)[1];
                    }

                    //var latestMovie = movies.Where(x => x.Downloaded == true).First();
                    // TODO: NO MOVIE DOWNLOADED == CRASHES
                    var response = new GetExistingMoviesResponse()
                    {
                        Movies = movies,
                        LatestMovie = latestMovie,
                        TotalPages = totalPages,
                        CurrentPage = request.CurrentPage
                    };

                    return response;
                } else
                {
                    return new GetExistingMoviesResponse
                    {
                        Movies = new List<Movie>(),
                        TotalPages = totalPages,
                        CurrentPage = request.CurrentPage
                    };
                }
            }
        }
    }
}
