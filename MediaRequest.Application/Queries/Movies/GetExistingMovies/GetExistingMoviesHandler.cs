using MediaRequest.Domain;
using MediaRequest.Domain.Configuration;
//using MediaRequest.Domain.Radarr;
using MediaRequest.Domain.API_Responses.Radarr.v3;
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
using MediaRequest.Application.Queries.Movies.GetTMDBContent;
using MediaRequest.Application.Clients;

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
        private readonly RadarrClient _radarrClient;

        public GetExistingMoviesHandler(RadarrClient radarrClient)
        {
            _radarrClient = radarrClient;
        }

        public async Task<GetExistingMoviesResponse> Handle(GetExistingMoviesRequest request, CancellationToken cancellationToken)
        {
            var model = new GetExistingMoviesResponse();
            var response = await _radarrClient.GetResponseCollection<Movie>("api/v3/movie");

            model.Movies = response;
            model.TotalPages = Functions.GetTotalPages(response, request.Amount);

            return model;
        }


        //private readonly IMediator _mediator;
        //private readonly IMediaDbContext _mediaDbContext;
        //private readonly ServicePath _path;
        //private readonly IHttpHelper _http;

        //public GetExistingMoviesHandler(IHttpHelper http, IOptions<ServicePath> path, IMediator mediator, IMediaDbContext mediaDbContext)
        //{
        //    _mediator = mediator;
        //    _mediaDbContext = mediaDbContext;
        //    _path = path.Value;
        //    _http = http;
        //}

        //public async Task<GetExistingMoviesResponse> Handle(GetExistingMoviesRequest request, CancellationToken cancellationToken)
        //{
        //    var results = request.Amount;
        //    var model = new GetExistingMoviesResponse()
        //    {
        //        CurrentPage = request.CurrentPage
        //    };

        //    var res = await _http.GetMovie();
        //    res.EnsureSuccessStatusCode();

        //    using(var stream = await res.Content.ReadAsStreamAsync())
        //    {
        //        var json = await JsonSerializer.DeserializeAsync<IEnumerable<Movie>>(stream, DefaultJsonSettings.Settings);
        //        model.Movies = json.Reverse();

        //        model.LatestMovie = model.Movies.Where(x => x.Downloaded == true).FirstOrDefault();
        //    }

        //    if(request.Amount > 0)
        //    {
        //        model.TotalPages = Functions.GetTotalPages(model.Movies, request.Amount);
        //        model.Movies = model.Movies.Skip(request.Amount * request.CurrentPage).Take(request.Amount).ToList();
        //    }

        //    foreach (var movie in model.Movies)
        //    {
        //        var existingPoster = _mediaDbContext.MoviePoster.FirstOrDefault(x => x.MovieId == movie.TMDBId.ToString());

        //        if (existingPoster == null)
        //        {
        //            var media = await _mediator.Send(new GetTMDBMediaRequest(movie.TMDBId));
        //            var posterPrefix = "https://image.tmdb.org/t/p/w500/";
        //            var fanartPrefix = "https://image.tmdb.org/t/p/original/";
        //            var fanart = fanartPrefix + media.Backdrops?.FirstOrDefault()?.FilePath;
        //            var poster = posterPrefix + media.Posters?.FirstOrDefault(x => x.iso_639_1 == "en")?.FilePath;
        //            var moviePoster = new MoviePoster
        //            {
        //                MovieId = movie.TMDBId.ToString(),
        //                FanartUrl = fanart,
        //                PosterUrl = poster
        //            };

        //            _mediaDbContext.MoviePoster.Add(moviePoster);
        //            await _mediaDbContext.SaveChangesAsync();

        //            movie.FanartUrl = fanart;
        //            movie.PosterUrl = poster;
        //        } else
        //        {
        //            movie.FanartUrl = existingPoster.FanartUrl;
        //            movie.PosterUrl = existingPoster.PosterUrl;
        //        }
        //    }

        //    return model;
        //}
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
            //    var results = 10;
            //    var model = new GetExistingMoviesResponse()
            //    {
            //        CurrentPage = request.CurrentPage
            //    };

            //    var res = await _http.GetMovie();
            //    res.EnsureSuccessStatusCode();

            //    using (var stream = await res.Content.ReadAsStreamAsync())
            //    {
            //        var json = await JsonSerializer.DeserializeAsync<IEnumerable<Movie>>(stream, DefaultJsonSettings.Settings);
            //        model.Movies = json;
            //        model.LatestMovie = model.Movies.Where(x => x.Downloaded == true).OrderByDescending(x => x.Added).First();
            //    }

            //    if(request.Input != null)
            //    {
            //        model.Movies = model.Movies.Where(x => x.Title.ToLower().Contains(request.Input.ToLower())).ToList();
            //    }

            //    if (request.FilterMode == 1)
            //    {
            //        model.Movies = model.Movies.Where(x => x.Downloaded == true).Reverse().ToList();
            //    } else if (request.FilterMode == 2)
            //    {
            //        model.Movies = model.Movies.Where(x => x.Downloaded == false).Reverse().ToList();
            //    }

            //    var totalPages = Functions.GetTotalPages(model.Movies as List<Movie>, results);
            //    model.Movies = model.Movies.Skip(results * request.CurrentPage).Take(results).ToList();


            //    if (model.Movies.Count() > 0)
            //    {
            //        var moviePosters = _context.MoviePoster.Where(x => model.Movies.Any(y => y.TMDBId.ToString() == x.MovieId)).ToList();

            //        foreach (var movie in model.Movies)
            //        {
            //            var existingPoster = _context.MoviePoster.FirstOrDefault(x => x.MovieId == movie.TMDBId.ToString());

            //            if (existingPoster == null)
            //            {
            //                var media = await _mediator.Send(new GetTMDBMediaRequest(movie.TMDBId));
            //                var posterPrefix = "https://image.tmdb.org/t/p/w500/";
            //                var fanartPrefix = "https://image.tmdb.org/t/p/original/";
            //                var fanart = fanartPrefix + media.Backdrops.FirstOrDefault()?.FilePath;
            //                var poster = posterPrefix + media.Posters.FirstOrDefault(x => x.iso_639_1 == "en")?.FilePath;
            //                var moviePoster = new MoviePoster
            //                {
            //                    MovieId = movie.TMDBId.ToString(),
            //                    FanartUrl = fanart,
            //                    PosterUrl = poster
            //                };

            //                _context.MoviePoster.Add(moviePoster);
            //                await _context.SaveChangesAsync();

            //                movie.FanartUrl = fanart;
            //                movie.PosterUrl = poster;
            //            }
            //            else
            //            {
            //                movie.FanartUrl = existingPoster.FanartUrl;
            //                movie.PosterUrl = existingPoster.PosterUrl;
            //            }
            //        }

            //        model.TotalPages = totalPages;

            //        return model;
            //    } else
            //    {
            //        return new GetExistingMoviesResponse
            //        {
            //            Movies = new List<Movie>(),
            //            TotalPages = totalPages,
            //            CurrentPage = request.CurrentPage
            //        };
            //    }

            throw new NotImplementedException();
        }
    }
}
