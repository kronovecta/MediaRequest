using MediaRequest.Domain.Configuration;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Application.Queries.Requests
{
    public class GetRequestsRequest : IRequest<GetRequestsResponse>
    {
        public IOptions<ApiKeys> ApiKeys { get; set; }
    }
}
