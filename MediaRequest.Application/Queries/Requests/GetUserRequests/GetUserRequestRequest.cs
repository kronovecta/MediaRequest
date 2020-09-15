using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Application.Queries.Requests.GetUserRequests
{
    public class GetUserRequestRequest : IRequest<GetUserRequestResponse>
    {
        public GetUserRequestRequest(string userid)
        {
            UserId = userid;
        }

        public string UserId { get; set; }
    }
}
