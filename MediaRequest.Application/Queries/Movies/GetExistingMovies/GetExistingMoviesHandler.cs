using MediaRequest.Domain;
using MediaRequest.Domain.Configuration;
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
using MediaRequest.Business.Extensions;
using MediaRequest.Application.Business.Enums;

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
            var results = 10;
            var model = new GetExistingMoviesResponse();
            var response = await _radarrClient.GetResponseCollection<Movie>("api/v3/movie");
            response = response.OrderByDescending(x => x.Added);

            if (request.Input != null)
            {
                response = response.Where(x => x.Title.ToLower().Contains(request.Input.ToLower())).ToList();
            }

            if (request.FilterMode == Filters.Downloaded)
            {
                response = response.Where(x => x.HasFile).ToList();
            }
            else if (request.FilterMode == Filters.Missing)
            {
                response = response.Where(x => !x.HasFile).ToList();
            }

            model.Movies = response.Skip(results * request.CurrentPage).Take(results).ToList();
            model.TotalPages = Functions.GetTotalPages(response, request.Amount);

            if (model.Movies.Count() > 0)
            {
                return model;
            }
            else
            {
                return new GetExistingMoviesResponse
                {
                    Movies = new List<Movie>(),
                    TotalPages = 1,
                    CurrentPage = request.CurrentPage
                };
            }
        }
    }

    public class GetExistingMoviesFilteredHandler : IRequestHandler<GetExistingMoviesFilteredRequest, GetExistingMoviesResponse>
    {
        private readonly RadarrClient _radarrClient;
        private readonly IMediaDbContext _context;

        public GetExistingMoviesFilteredHandler(RadarrClient radarrClient, IMediaDbContext context)
        {
            _radarrClient = radarrClient;
            _context = context;
        }

        public async Task<GetExistingMoviesResponse> Handle(GetExistingMoviesFilteredRequest request, CancellationToken cancellationToken)
        {
            var results = 10;
            var model = new GetExistingMoviesResponse()
            {
                CurrentPage = request.CurrentPage
            };

            var res = await _radarrClient.GetResponseCollection<Movie>("api/v3/movie");
            model.Movies = res;

            if (request.Input != null)
            {
                model.Movies = model.Movies.Where(x => x.Title.ToLower().Contains(request.Input.ToLower())).ToList();
            }

            if (request.FilterMode == 1)
            {
                model.Movies = model.Movies.Where(x => x.HasFile == true).Reverse().ToList();
            }
            else if (request.FilterMode == 2)
            {
                model.Movies = model.Movies.Where(x => x.HasFile == false).Reverse().ToList();
            }

            var totalPages = Functions.GetTotalPages(model.Movies as List<Movie>, results);
            model.Movies = model.Movies.Skip(results * request.CurrentPage).Take(results).ToList();


            if (model.Movies.Count() > 0)
            {
                var moviePosters = _context.MoviePoster.Where(x => model.Movies.Any(y => y.TmdbId.ToString() == x.MovieId)).ToList();

                model.TotalPages = totalPages;

                return model;
            }
            else
            {
                return new GetExistingMoviesResponse
                {
                    Movies = new List<Movie>(),
                    TotalPages = totalPages,
                    CurrentPage = request.CurrentPage
                };
            }

            throw new NotImplementedException();
        }
    }
}
