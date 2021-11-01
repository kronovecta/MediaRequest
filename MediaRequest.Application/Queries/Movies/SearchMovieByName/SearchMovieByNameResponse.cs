using MediaRequest.Domain;
using MediaRequest.Domain.API_Responses.Radarr.v3;
using System.Collections.Generic;

namespace MediaRequest.Application.Queries.Movies.SearchMovieByName
{
    public class SearchMovieByNameResponse
    {
        public int SearchResults { get; set; }
        public List<Movie> Movies { get; set; }
    }
}