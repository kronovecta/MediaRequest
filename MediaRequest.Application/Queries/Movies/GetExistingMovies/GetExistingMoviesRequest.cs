using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Application.Queries.Movies.GetExistingMovies
{
    public class GetExistingMoviesRequest : IRequest<GetExistingMoviesResponse>
    {
        public string ApiKey_Radarr { get; set; }
        public string ApiKey_TMDB { get; set; }
    }

    public class GetExistingMoviesFilteredRequest : IRequest<GetExistingMoviesResponse>
    {
        public string ApiKey_Radarr { get; set; }
        public string ApiKey_TMDB { get; set; }
        public string input { get; set; }
    }
}
