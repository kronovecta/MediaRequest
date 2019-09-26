using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Queries.Requests
{
    public class GetRequestsHandler : IRequestHandler<GetRequestsRequest, GetRequestsResponse>
    {
        private readonly IMediaDbContext _context;

        public GetRequestsHandler(IMediaDbContext context)
        {
            _context = context;
        }

        public async Task<GetRequestsResponse> Handle(GetRequestsRequest request, CancellationToken cancellationToken)
        {
            var requests = await _context.Request.ToListAsync();

            var response = new GetRequestsResponse() { Requests = requests };

            return response;
        }
    }
}
