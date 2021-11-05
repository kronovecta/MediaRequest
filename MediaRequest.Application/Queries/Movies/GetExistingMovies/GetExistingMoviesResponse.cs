using MediaRequest.Domain;
using MediaRequest.Domain.API_Responses.Radarr.v3;
using MediaRequest.Domain.Interfaces;
using System.Collections.Generic;

namespace MediaRequest.Application.Queries.Movies
{
    public class GetExistingMoviesResponse : IRequestResponse
    {
        public IEnumerable<Movie> Movies { get; set; }
        public Movie LatestMovie { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}