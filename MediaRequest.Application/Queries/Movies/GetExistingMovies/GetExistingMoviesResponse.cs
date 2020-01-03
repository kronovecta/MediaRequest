using MediaRequest.Domain;
using MediaRequest.Domain.Radarr;
using System.Collections.Generic;

namespace MediaRequest.Application.Queries.Movies
{
    public class GetExistingMoviesResponse
    {
        public IEnumerable<Movie> Movies { get; set; }
        public Movie LatestMovie { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}