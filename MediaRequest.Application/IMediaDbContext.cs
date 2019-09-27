using MediaRequest.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
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
