using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Queries.Requests.GetSingleRequest
{
    public class RequestExistsHandler : IRequestHandler<RequestExistsRequest, RequestExistsResponse>
    {
        private readonly IMediaDbContext _context;

        public RequestExistsHandler(IMediaDbContext context)
        {
            _context = context;
        }

        private bool AcceptedRequest(string movieid)
        {
            var requests = _context.Request.Where(x => x.Status == true && x.MovieId == movieid);
            if (requests.Count() > 0)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public async Task<RequestExistsResponse> Handle(RequestExistsRequest request, CancellationToken cancellationToken)
        {
            var accepted = AcceptedRequest(request.Movie.TMDBId.ToString());
            var exists = await _context.Request.AnyAsync(x => x.MovieId == request.Movie.TMDBId.ToString() && x.UserId == request.UserId);
            return new RequestExistsResponse { Exists = exists, Accepted = accepted };
        }
    }
}
