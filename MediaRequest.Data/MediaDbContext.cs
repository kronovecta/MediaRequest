using MediaRequest.Application;
using MediaRequest.Domain;
using MediaRequest.Domain.Radarr;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<UserRequest>(e =>
            //{
            //    e.HasKey(k => k.RequestId);
            //});

            //builder.Entity<Movie>(e =>
            //{
            //    e.HasKey(x => x.Id);

            //    e.Ignore(x => x.Tags);
            //    e.Ignore(x => x.AlternativeTitles);
            //    e.Ignore(x => x.Genres);
            //    e.Ignore(x => x.Images);
            //    e.Ignore(x => x.Ratings);
            //});
        }
    }
}
