using MediaRequest.Domain;
using System.Collections.Generic;

namespace MediaRequest.Application.Queries.Requests
{
    public class GetRequestsResponse
    {
        public List<UserRequest> Requests { get; set; }
    }
}