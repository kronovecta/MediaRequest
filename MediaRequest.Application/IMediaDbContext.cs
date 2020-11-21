using MediaRequest.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading.Tasks;

namespace MediaRequest.Application
{
    public interface IMediaDbContext
    {
        DbSet<UserRequest> Request { get; set; }
        DbSet<MoviePoster> MoviePoster { get; set; }


        Task<int> SaveChangesAsync();
    }
}
