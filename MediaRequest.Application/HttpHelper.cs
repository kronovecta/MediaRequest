using MediaRequest.Application.Clients;
using MediaRequest.Application.Queries;
using MediaRequest.Application.Queries.Movies;
using MediaRequest.Application.Queries.Movies.GetSingleExistingMovie;
using MediaRequest.Application.Queries.Movies.GetTMDBContent;
using MediaRequest.Application.Queries.People.GetPopularMovies;
using MediaRequest.Domain;
using MediaRequest.Domain.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MediaRequest.Application
{
    public class HttpHelper : IHttpHelper
    {
        private readonly ApiKeys _keys;
        private readonly ServicePath _path;
        private readonly RadarrClient _radarrClient;
        private readonly TMDBClient _tmdbClient;

        #region Movies
        public HttpHelper(IOptions<ServicePath> path, IOptions<ApiKeys> keys, RadarrClient radarrClient, TMDBClient tmdbClient)
        {
            _keys = keys.Value;
            _path = path.Value;
            
            _radarrClient = radarrClient;
            _tmdbClient = tmdbClient;
        }

        public async Task<HttpResponseMessage> GetMovie()
        {
            return await _radarrClient.Client.GetAsync($"api/movie?apikey={_keys.Radarr}");
        }

        public async Task<HttpResponseMessage> GetMovie(GetSingleMovieRequest request)
        {
            return await _radarrClient.Client.GetAsync($"api/movie/lookup/tmdb?apikey={_keys.Radarr}&tmdbId={request.TmdbId}");
        }

        public async Task<HttpResponseMessage> GetMovie(GetMovieMediaRequest request)
        {
            return await _radarrClient.Client.GetAsync($"api/movie/lookup/tmdb?apikey={_keys.Radarr}&tmdbId={request.TMDBId}");
        }

        public async Task<HttpResponseMessage> GetMovie(GetSingleExistingMovieRequest request)
        {
            return await _radarrClient.Client.GetAsync($"api/movie/{request.RadarrMovieId}?apikey={_keys.Radarr}");
        }

        public async Task<HttpResponseMessage> GetPopularMovies(string actorid)
        {
            return await _radarrClient.Client.GetAsync($"api/movie/{actorid}?apikey={_keys.Radarr}");
        }
        #endregion

        #region Misc
        public Task<HttpResponseMessage> GetDetails(GetMovieDetailsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> GetCast(GetCreditsRequest request)
        {
            switch (request.MediaType)
            {
                case MediaType.Movie:
                    return await _tmdbClient.Client.GetAsync($"{request.MediaType.ToString().ToLower()}/{request.TMDBId}/credits");
                case MediaType.Tv:
                    return await _tmdbClient.Client.GetAsync($"{request.MediaType.ToString().ToLower()}/{request.TMDBId}/aggregate_credits");
                default:
                    throw new GetCreditsException("There was an error retrieving the cast for the given title");
            }
        }

        public async Task<HttpResponseMessage> GetRecommended(GetRecommendedRequest request)
        {
            return await _tmdbClient.Client.GetAsync($"movie/{request.TMDBId}/recommendations?api_key=" + _keys.TMDB);
        }

        public async Task<HttpResponseMessage> GetUpcoming(int? days)
        {
            string span = days > 0 ? DateTime.Now.AddDays(days ?? 30).ToShortDateString() : DateTime.Now.AddDays(30).ToShortDateString();

            return await _radarrClient.Client.GetAsync($"api/calendar?start={DateTime.Now.ToShortDateString()}&end={span}&apikey=" + _keys.Radarr);
        }

        public async Task<HttpResponseMessage> GetDetails(string actorid)
        {
            return await _tmdbClient.Client.GetAsync("person/" + actorid + "?api_key=" + _keys.TMDB);
        }

        public async Task<HttpResponseMessage> GetCombinedMedia(string actorid)
        {
            return await _tmdbClient.Client.GetAsync("person/" + actorid + "/movie_credits?api_key=" + _keys.TMDB);
        }

        public async Task<HttpResponseMessage> GetPopularMovies(string actorid, MediaType mediaType = MediaType.Movie)
        {
            return await _tmdbClient.Client.GetAsync($"discover/{mediaType}?with_cast={actorid}&sort_by=popularity.desc&api_key={_keys.TMDB}");
        }

        public async Task<HttpResponseMessage> GetTMDBMedia(GetTMDBMediaRequest request)
        {
            return await _tmdbClient.Client.GetAsync(string.Format("movie/{0}/images?api_key={1}", request.TMDB, _keys.TMDB));
        }
        #endregion
    }
}
