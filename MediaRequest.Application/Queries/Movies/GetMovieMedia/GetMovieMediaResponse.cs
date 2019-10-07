using MediaRequest.Domain.TMDB;

namespace MediaRequest.Application.Queries.Movies
{
    public class GetMovieMediaResponse
    {
        public TMDBMovie Movie { get; set; }
    }
}