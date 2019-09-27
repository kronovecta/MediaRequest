using MediaRequest.Domain;
using MediaRequest.Domain.Radarr;
using System.Collections.Generic;

namespace MediaRequest.Application.Queries.Movies.SearchMovieByName
{
    public class SearchMovieByNameResponse
    {
        public int SearchResults { get; set; }
        public List<Movie> Movies { get; set; }
    }
}