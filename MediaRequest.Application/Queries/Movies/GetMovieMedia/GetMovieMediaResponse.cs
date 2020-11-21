using MediaRequest.Domain.Radarr;
using MediaRequest.Domain.TMDB;

namespace MediaRequest.Application.Queries.Movies
{
    public class GetMovieMediaResponse
    {
        public Movie Movie { get; set; }
    }
}