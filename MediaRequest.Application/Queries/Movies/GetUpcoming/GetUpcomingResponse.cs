using MediaRequest.Domain.Radarr;
using System.Collections.Generic;

namespace MediaRequest.Application.Queries.Movies.GetUpcoming
{
    public class GetUpcomingResponse
    {
        public IEnumerable<Movie> Movies { get; set; }
    }
}