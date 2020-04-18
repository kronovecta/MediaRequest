using MediaRequest.Application;
using MediaRequest.Application.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.Services
{
    public class RequestServices 
    {
        private readonly IMediaDbContext _context;

        public RequestServices(IMediaDbContext context)
        {
            _context = context;
        }

        public async Task<RequestResponse> AddRequestAsync(string tmdbid, string userid)
        {
            var requests = await _context.Request.ToListAsync();

            if (await _context.Request.Where(x => x.UserId == userid && x.MovieId == tmdbid).CountAsync() > 0)
            {
                return new RequestResponse { Success = false, Message = "Movie already requested by user" };
            }

            var res = await _context.Request.AddAsync(new Domain.UserRequest() { UserId = userid, MovieId = tmdbid, Status = false });
            await _context.SaveChangesAsync();

            return new RequestResponse { Message = "Request added", Success = true };

        }
    }

    public class RequestResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
