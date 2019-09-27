using MediaRequest.Domain;
using MediaRequest.Domain.Radarr;

namespace MediaRequest.Application.Queries
{
    public class GetSingleMovieResponse
    {
        public Movie Movie { get; set; }
    }
}