using MediaRequest.Application;
using MediaRequest.Domain;
using MediaRequest.Domain.Radarr;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Data
{
    public class MediaDbContext : DbContext, IMediaDbContext
    {
        public MediaDbContext(DbContextOptions<MediaDbContext> options) : base(options)
        {
            
        }

        public DbSet<UserRequest> Request { get; set; }
        public DbSet<MoviePoster> MoviePoster { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected void OnModelCreation(ModelBuilder builder)
        {
            builder.Entity<UserRequest>().HasData(new UserRequest { UserId = "a4ab47cc-ddf1-419a-90eb-3e86dd8fa50c", MovieId = "181812", Status = false });
        }
    }
}
