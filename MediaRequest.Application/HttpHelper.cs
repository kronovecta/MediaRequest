﻿using MediaRequest.Application.Queries;
using MediaRequest.Application.Queries.Movies;
using MediaRequest.Application.Queries.Movies.GetSingleExistingMovie;
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
        private static HttpClient Client = new HttpClient();
        private readonly ApiKeys _keys;
        private readonly ServicePath _path;

        #region Movies
        public HttpHelper(IOptions<ServicePath> path, IOptions<ApiKeys> keys)
        {
            _keys = keys.Value;
            _path = path.Value;
        }

        public async Task<HttpResponseMessage> GetMovie()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(_path.Radarr + $"/api/movie?apikey={_keys.Radarr}");

                return response;
            }
        }

        public async Task<HttpResponseMessage> GetMovie(GetSingleMovieRequest request)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(_path.Radarr + $"/api/movie/lookup/tmdb?apikey={_keys.Radarr}&tmdbId={request.TmdbId}");
                return response;
            }
        }

        public async Task<HttpResponseMessage> GetMovie(GetMovieMediaRequest request)
        {
            using (var client = new HttpClient())
            {
                //var result = await client.GetAsync(_path.TMDB + $"/movie/{request.TMDBId}?api_key={_keys.TMDB}");
                var result = await client.GetAsync($"{_path.Radarr}/api/movie/lookup/tmdb?apikey=fc2c71c89e9b42cf99c4bd4d215632b0&tmdbId={request.TMDBId}");

                return result;
            }
        }

        public async Task<HttpResponseMessage> GetMovie(GetSingleExistingMovieRequest request)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync($"{_path.Radarr}/api/movie/{request.RadarrMovieId}?apikey={_keys.Radarr}");

                return result;
            }
        }
        #endregion

        #region Misc
        public Task<HttpResponseMessage> GetDetails(GetMovieDetailsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> GetCast(GetCreditsRequest request)
        {
            var response = await Client.GetAsync(_path.TMDB + $"/movie/{request.TMDBId}/credits?api_key=" + _keys.TMDB);
            return response;
        }

        public async Task<HttpResponseMessage> GetRecommended(GetRecommendedRequest request)
        {
            var response = await Client.GetAsync(_path.TMDB + $"/movie/{request.TMDBId}/recommendations?api_key=" + _keys.TMDB);
            return response;
        }

        public async Task<HttpResponseMessage> GetUpcoming(int? days)
        {
            string span = "";

            if(days > 0)
            {
                span = DateTime.Now.AddDays(days ?? 30).ToShortDateString();
            } else
            {
                span = DateTime.Now.AddDays(30).ToShortDateString();
            }
            
            var response = await Client.GetAsync(_path.Radarr + $"/api/calendar?end={span}&apikey=" + _keys.Radarr);
            return response;
        }

        public async Task<HttpResponseMessage> GetDetails(string actorid)
        {
            var response = await Client.GetAsync(_path.TMDB + "/person/" + actorid + "?api_key=" + _keys.TMDB);
            return response;
        }

        public async Task<HttpResponseMessage> GetCombinedMedia(string actorid)
        {
            var response = await Client.GetAsync(_path.TMDB + "/person/" + actorid + "/movie_credits?api_key=" + _keys.TMDB);
            return response;
        }

        #endregion
    }
}
