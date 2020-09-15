using MediaRequest.Domain.API_Responses.TMDB;
using System.Collections.Generic;

namespace MediaRequest.Application.Queries.People.GetPopularMovies
{
    public class GetPopularMoviesResponse
    {
        public IEnumerable<PopularMovie> Movies { get; set; }
    }
}