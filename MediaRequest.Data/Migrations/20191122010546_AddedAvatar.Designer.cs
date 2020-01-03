﻿// <auto-generated />
using MediaRequest.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MediaRequest.Data.Migrations
{
    [DbContext(typeof(MediaDbContext))]
    [Migration("20191122010546_AddedAvatar")]
    partial class AddedAvatar
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("MediaRequest.Domain.MoviePoster", b =>
                {
                    b.Property<int>("MoviePosterId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FanartUrl");

                    b.Property<string>("MovieId");

                    b.Property<string>("PosterUrl");

                    b.HasKey("MoviePosterId");

                    b.ToTable("MoviePoster");
                });

            modelBuilder.Entity("MediaRequest.Domain.UserRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MovieId");

                    b.Property<bool>("Status");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Request");
                });
#pragma warning restore 612, 618
        }
    }
}
