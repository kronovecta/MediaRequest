﻿using MediaRequest.Application.Queries;
using MediaRequest.Application.Queries.Movies;
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
                var result = await client.GetAsync(_path.TMDB + $"/movie/{request.TMDBId}?api_key={_keys.TMDB}");

                return result;
            }
        }
        #endregion

        #region Misc
        public Task<HttpResponseMessage> GetDetails(GetMovieDetailsRequest request)
        {
            //using (var client = new HttpClient())
            //{
            //    var response = client.GetAsync()
            //}

            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> GetCast(GetCreditsRequest request)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(_path.TMDB + $"/movie/{request.TMDBId}/credits?api_key=" + _keys.TMDB);
                return response;
            }
        }
        #endregion
    }
}