using MediaRequest.Domain;
using MediaRequest.Domain.Radarr;
using MediaRequest.Domain.TMDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Queries.Movies.GetExistingMovies
{
    public class GetExistingMoviesHandler : IRequestHandler<GetExistingMoviesRequest, GetExistingMoviesResponse>
    {
        private readonly IMediaDbContext _context;

        public GetExistingMoviesHandler(IMediaDbContext context)
        {
            _context = context;
        }

        public async Task<GetExistingMoviesResponse> Handle(GetExistingMoviesRequest request, CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            {
                var res = await client.GetAsync($"https://tiger.seedhost.eu/robert/radarr/api/movie?apikey={request.ApiKey_Radarr}");
                res.EnsureSuccessStatusCode();

                var result = await res.Content.ReadAsStringAsync();
                var movies = (JsonConvert.DeserializeObject<IEnumerable<Movie>>(result).OrderByDescending(x => x.Id));

                var moviePosters = await _context.MoviePoster.ToListAsync<MoviePoster>();

                var latestMovie = movies.Where(x => x.Downloaded == true).First();


                foreach (var movie in movies)
                {
                    if (moviePosters.Any(x => x.MovieId == movie.TMDBId))
                    {
                        var poster = await _context.MoviePoster.SingleOrDefaultAsync(x => x.MovieId == movie.TMDBId);
                        if (poster != null && poster.PosterUrl != "")
                            movie.PosterUrl = poster.PosterUrl;
                            movie.FanartUrl = poster.FanartUrl;
                    }
                    else
                    {
                        using (var tmdbclient = new HttpClient())
                        {
                            var tmdb_response = await tmdbclient.GetAsync($"https://api.themoviedb.org/3/movie/{movie.TMDBId}?api_key={request.ApiKey_TMDB}");
                            var tmdb_string = await tmdb_response.Content.ReadAsStringAsync();
                            var tmdb_movie = (JsonConvert.DeserializeObject<TMDBMovie>(tmdb_string));
                            var poster_path = $"https://image.tmdb.org/t/p/w500{tmdb_movie.poster_path}";
                            var fanart_path = $"https://image.tmdb.org/t/p/original{tmdb_movie.backdrop_path}";

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

                var response = new GetExistingMoviesResponse
                {
                    Movies = movies,
                    LatestMovie = latestMovie
                };

                return response;
            }
        }
    }
}
