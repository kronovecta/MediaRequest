using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Queries.Requests.GetUserRequests
{
    class GetUserRequestHandler : IRequestHandler<GetUserRequestRequest, GetUserRequestResponse>
    {
        private readonly IMediaDbContext _context;

        public GetUserRequestHandler(IMediaDbContext context)
        {
            _context = context;
        }

        public async Task<GetUserRequestResponse> Handle(GetUserRequestRequest request, CancellationToken cancellationToken)
        {
            var requests = await _context.Request.Where(x => x.UserId == request.UserId).ToListAsync();
            return new GetUserRequestResponse() { Requests = requests };
        }
    }
}
