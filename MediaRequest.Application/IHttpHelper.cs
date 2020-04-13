using MediaRequest.Application.Queries.Movies;
using MediaRequest.Application.Queries.Movies.GetSingleExistingMovie;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MediaRequest.Application
{
    public interface IHttpHelper
    {
        Task<HttpResponseMessage> GetMovie();
        // Retrieves all existing movies
        Task<HttpResponseMessage> GetMovie(GetSingleMovieRequest request);
        Task<HttpResponseMessage> GetMovie(GetMovieMediaRequest request);
        Task<HttpResponseMessage> GetMovie(GetSingleExistingMovieRequest request);
        Task<HttpResponseMessage> GetDetails(GetMovieDetailsRequest request);
        Task<HttpResponseMessage> GetCast(GetCreditsRequest request);
        Task<HttpResponseMessage> GetRecommended(GetRecommendedRequest request);
        Task<HttpResponseMessage> GetUpcoming(int? days);
        Task<HttpResponseMessage> GetDetails(string actorid);
        Task<HttpResponseMessage> GetCombinedMedia(string actorid);
        Task<HttpResponseMessage> GetPopularMovies(string actorid);
    }
}
