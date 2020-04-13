using MediaRequest.Domain;
using MediaRequest.Domain.Configuration;
using MediaRequest.Domain.Radarr;
using MediaRequest.Domain.TMDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using MediaRequest.Application.Parsers;

namespace MediaRequest.Application.Queries.Movies
{
    static class Functions
    {
        public static int GetTotalPages(IEnumerable<Movie> movies, int results)
        {
            return movies.Count() % results != 0 ? movies.Count() / results : movies.Count() / results - 1;
        }
    }

    public class GetExistingMoviesHandler : IRequestHandler<GetExistingMoviesRequest, GetExistingMoviesResponse>
    {
        private readonly IMediator _mediator;
        private readonly ServicePath _path;
        private readonly IHttpHelper _http;

        public GetExistingMoviesHandler(IHttpHelper http, IOptions<ServicePath> path, IMediator mediator)
        {
            _mediator = mediator;
            _path = path.Value;
            _http = http;
        }

        public async Task<GetExistingMoviesResponse> Handle(GetExistingMoviesRequest request, CancellationToken cancellationToken)
        {
            var results = request.Amount;
            var model = new GetExistingMoviesResponse()
            {
                CurrentPage = request.CurrentPage
            };

            var res = await _http.GetMovie();
            res.EnsureSuccessStatusCode();

            using(var stream = await res.Content.ReadAsStreamAsync())
            {
                var json = await JsonSerializer.DeserializeAsync<IEnumerable<Movie>>(stream, DefaultJsonSettings.Settings);
                model.Movies = json.Reverse();

                model.LatestMovie = model.Movies.Where(x => x.Downloaded == true).First();
            }

            var totalPages = 0;

            if(request.Amount > 0)
            {
                model.TotalPages = Functions.GetTotalPages(model.Movies, request.Amount);
                model.Movies = model.Movies.Skip(request.Amount * request.CurrentPage).Take(request.Amount).ToList();
            }
                
            foreach (var movie in model.Movies)
            {
                if (movie.Images.Any(x => x.CoverType == "poster"))
                {
                    movie.PosterUrl = _path.BaseURL + movie.Images.Where(x => x.CoverType == "poster").First().URL;
                } else
                {
                    var tmdbmovie = await _mediator.Send(new GetMovieMediaRequest { TMDBId = movie.TMDBId.ToString() });
                    movie.PosterUrl = tmdbmovie.Movie.Images.Where(x => x.CoverType == "poster").First().URL;
                }

                if (movie.Images.Any(x => x.CoverType == "fanart"))
                {
                    movie.FanartUrl = _path.BaseURL + movie.Images.Where(x => x.CoverType == "fanart").First().URL;
                }
                else
                {
                    var tmdbmovie = await _mediator.Send(new GetMovieMediaRequest { TMDBId = movie.TMDBId.ToString() });
                    movie.FanartUrl = tmdbmovie.Movie.Images.Where(x => x.CoverType == "fanart").First().URL;
                }
            }

            return model;
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
            var model = new GetExistingMoviesResponse()
            {
                CurrentPage = request.CurrentPage
            };

            var res = await _http.GetMovie();
            res.EnsureSuccessStatusCode();

            using (var stream = await res.Content.ReadAsStreamAsync())
            {
                var json = await JsonSerializer.DeserializeAsync<IEnumerable<Movie>>(stream, DefaultJsonSettings.Settings);
                model.Movies = json;
                model.LatestMovie = model.Movies.Where(x => x.Downloaded == true).First();
            }

            if(request.Input != null)
            {
                model.Movies = model.Movies.Where(x => x.Title.ToLower().Contains(request.Input.ToLower())).ToList();
            }

            if (request.FilterMode == 1)
            {
                model.Movies = model.Movies.Where(x => x.Downloaded == true).Reverse().ToList();
            }

            else if (request.FilterMode == 2)
            {
                model.Movies = model.Movies.Where(x => x.Downloaded == false).Reverse().ToList();
            }

            var totalPages = Functions.GetTotalPages(model.Movies as List<Movie>, results);
            model.Movies = model.Movies.Skip(results * request.CurrentPage).Take(results).ToList();


            if (model.Movies.Count() > 0)
            {
                var moviePosters = _context.MoviePoster.Where(x => model.Movies.Any(y => y.TMDBId.ToString() == x.MovieId)).ToList();

                foreach (var movie in model.Movies)
                {
                    if (movie.Images.Any(x => x.CoverType == "poster"))
                    {
                        movie.PosterUrl = _path.BaseURL + movie.Images.Where(x => x.CoverType == "poster").First().URL;
                    }
                    else
                    {
                        var tmdbmovie = await _mediator.Send(new GetMovieMediaRequest { TMDBId = movie.TMDBId.ToString() });
                        movie.PosterUrl = tmdbmovie.Movie.Images.Where(x => x.CoverType == "poster").First().URL;
                    }

                    if (movie.Images.Any(x => x.CoverType == "fanart"))
                    {
                        movie.FanartUrl = _path.BaseURL + movie.Images.Where(x => x.CoverType == "fanart").First().URL;
                    }
                    else
                    {
                        var tmdbmovie = await _mediator.Send(new GetMovieMediaRequest { TMDBId = movie.TMDBId.ToString() });
                        movie.FanartUrl = tmdbmovie.Movie.Images.Where(x => x.CoverType == "fanart").First().URL;
                    }
                }

                model.TotalPages = totalPages;

                return model;
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
