using MediaRequest.Domain.Configuration;
using MediatR;

namespace MediaRequest.Application.Queries
{
    public class GetSingleMovieRequest : IRequest<GetSingleMovieResponse>
    {
        public ApiKeys Keys { get; set; }
        public string TmdbId { get; set; }
    }
}
