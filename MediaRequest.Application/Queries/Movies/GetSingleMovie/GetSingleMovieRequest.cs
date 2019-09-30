using MediatR;

namespace MediaRequest.Application.Queries
{
    public class GetSingleMovieRequest : IRequest<GetSingleMovieResponse>
    {
        public string ApiKey { get; set; }
        public string TmdbId { get; set; }
    }
}
