using MediaRequest.Domain.API_Responses.Radarr.v3;
using System.Collections.Generic;

namespace MediaRequest.Application.Queries.Movies.GetUpcoming
{
    public class GetUpcomingResponse
    {
        public IEnumerable<Movie> Movies { get; set; }
    }
}