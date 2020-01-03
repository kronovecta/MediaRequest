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
        private readonly IMediator _mediator;
        private readonly ApiKeys _keys;
        private readonly ServicePath _path;
        private readonly IHttpHelper _http;

        public GetExistingMoviesHandler(IHttpHelper http, IMediaDbContext context, IOptions<ServicePath> path, IOptions<ApiKeys> keys, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
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
                
                foreach (var movie in movies)
                {
                    if (movie.Images.Any(x => x.CoverType == "poster"))
                    {
                        movie.PosterUrl = _path.BaseURL + movie.Images.Where(x => x.CoverType == "poster").First().URL;
                    } else
                    {
                        var tmdbmovie = await _mediator.Send(new GetMovieMediaRequest { TMDBId = movie.TMDBId });
                        movie.PosterUrl = tmdbmovie.Movie.Images.Where(x => x.CoverType == "poster").First().URL;
                    }

                    if (movie.Images.Any(x => x.CoverType == "fanart"))
                    {
                        movie.FanartUrl = _path.BaseURL + movie.Images.Where(x => x.CoverType == "fanart").First().URL;
                    }
                    else
                    {
                        var tmdbmovie = await _mediator.Send(new GetMovieMediaRequest { TMDBId = movie.TMDBId });
                        movie.FanartUrl = tmdbmovie.Movie.Images.Where(x => x.CoverType == "fanart").First().URL;
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
        private readonly IMediator _mediator;
        private readonly ServicePath _path;
        private readonly IHttpHelper _http;

        public GetExistingMoviesFilteredHandler(IHttpHelper http, IMediaDbContext context, IOptions<ServicePath> path, IMediator mediator)
        {
            _path = path.Value;
            _context = context;
            _mediator = mediator;
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
                    movies = movies.Where(x => x.Title.ToLower().Contains(request.Input.ToLower())).ToList();
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


                if (movies.Count() > 0)
                {
                    var moviePosters = _context.MoviePoster.Where(x => movies.Any(y => y.TMDBId == x.MovieId)).ToList();

                    foreach (var movie in movies)
                    {
                        if (movie.Images.Any(x => x.CoverType == "poster"))
                        {
                            movie.PosterUrl = _path.BaseURL + movie.Images.Where(x => x.CoverType == "poster").First().URL;
                        }
                        else
                        {
                            var tmdbmovie = await _mediator.Send(new GetMovieMediaRequest { TMDBId = movie.TMDBId });
                            movie.PosterUrl = tmdbmovie.Movie.Images.Where(x => x.CoverType == "poster").First().URL;
                        }

                        if (movie.Images.Any(x => x.CoverType == "fanart"))
                        {
                            movie.FanartUrl = _path.BaseURL + movie.Images.Where(x => x.CoverType == "fanart").First().URL;
                        }
                        else
                        {
                            var tmdbmovie = await _mediator.Send(new GetMovieMediaRequest { TMDBId = movie.TMDBId });
                            movie.FanartUrl = tmdbmovie.Movie.Images.Where(x => x.CoverType == "fanart").First().URL;
                        }
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
