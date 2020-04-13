using MediaRequest.Domain;
using System.Collections.Generic;

namespace MediaRequest.Application.Queries.Requests.GetUserRequests
{
    public class GetUserRequestResponse
    {
        public List<UserRequest> Requests { get; set; }
    }
}