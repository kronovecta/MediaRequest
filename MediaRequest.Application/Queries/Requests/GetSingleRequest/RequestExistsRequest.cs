using MediaRequest.Domain.Radarr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Application.Queries.Requests.GetSingleRequest
{
    public class RequestExistsRequest : IRequest<RequestExistsResponse>
    {
        public Movie Movie { get; set; }
        public string UserId { get; set; }
    }
}
