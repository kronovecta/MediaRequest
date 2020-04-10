using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Commands.CancelRequest
{
    public class CancelRequestHandler : IRequestHandler<CancelRequestCommand>
    {
        private readonly IMediaDbContext _context;

        public CancelRequestHandler(IMediaDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CancelRequestCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var request = await _context.Request.SingleOrDefaultAsync(x => x.Id == command.RequestID);
                if (request == null)
                {
                    throw new Exception("Request returned no results");
                }
                else
                {
                    _context.Request.Remove(request);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return new Unit();
        }
    }
}
