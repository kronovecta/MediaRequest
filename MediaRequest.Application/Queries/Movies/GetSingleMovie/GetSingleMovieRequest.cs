using MediaRequest.Domain.Configuration;
using MediatR;

namespace MediaRequest.Application.Queries.Movies
{
    public class GetSingleMovieRequest : IRequest<GetSingleMovieResponse>
    {
        public string TmdbId { get; set; }
    }
}
