using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Commands
{
    public class AddRequestHandler : IRequestHandler<AddRequestCommand>
    {
        private readonly IMediaDbContext _context;

        public AddRequestHandler(IMediaDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddRequestCommand request, CancellationToken cancellationToken)
        {
            //var providers = _context.NotificationProvider.Where(x => x.UserId == request.Request.UserId);

            var res = await _context.Request.AddAsync(request.Request);
            await _context.SaveChangesAsync();

            return new Unit();
        }
    }
}
