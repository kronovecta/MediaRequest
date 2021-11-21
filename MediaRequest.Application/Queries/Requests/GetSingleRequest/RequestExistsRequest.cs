using MediaRequest.Domain.API_Responses.Radarr.v3;
using MediatR;

namespace MediaRequest.Application.Queries.Requests.GetSingleRequest
{
    public class RequestExistsRequest : IRequest<RequestExistsResponse>
    {
        public Movie Movie { get; set; }
        public string UserId { get; set; }
    }
}
