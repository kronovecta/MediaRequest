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
        public DbSet<InviteToken> InviteTokens { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<InviteToken>(x =>
            {
                x.HasKey(key => key.Id);
                x.HasIndex(ind => ind.Id);
            });
        }
    }
}
