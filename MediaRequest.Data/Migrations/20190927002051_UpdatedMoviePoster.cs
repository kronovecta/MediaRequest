using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaRequest.Data.Migrations
{
    public partial class UpdatedMoviePoster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "Request",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Movie",
                table: "MoviePoster",
                newName: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Request",
                newName: "RequestId");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "MoviePoster",
                newName: "Movie");

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<double>(nullable: false),
                    Votes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Added = table.Column<DateTime>(nullable: false),
                    Downloaded = table.Column<bool>(nullable: false),
                    FolderName = table.Column<string>(nullable: true),
                    HasFile = table.Column<bool>(nullable: false),
                    InCinemas = table.Column<DateTime>(nullable: false),
                    IsAvailable = table.Column<bool>(nullable: false),
                    MinimumAvailability = table.Column<string>(nullable: true),
                    Monitored = table.Column<bool>(nullable: false),
                    Overview = table.Column<string>(nullable: true),
                    PathState = table.Column<string>(nullable: true),
                    ProfileId = table.Column<int>(nullable: false),
                    QualityProfileId = table.Column<int>(nullable: false),
                    RatingsId = table.Column<int>(nullable: true),
                    RemotePoster = table.Column<string>(nullable: true),
                    Runtime = table.Column<int>(nullable: false),
                    SecondaryYearSourceId = table.Column<int>(nullable: false),
                    SizeOnDisk = table.Column<string>(nullable: true),
                    SortTitle = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    TMDBId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    TitleSlug = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movie_Ratings_RatingsId",
                        column: x => x.RatingsId,
                        principalTable: "Ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlternativeTitle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    language = table.Column<string>(nullable: true),
                    movieId = table.Column<int>(nullable: false),
                    sourceId = table.Column<int>(nullable: false),
                    sourceType = table.Column<string>(nullable: true),
                    title = table.Column<string>(nullable: true),
                    voteCount = table.Column<int>(nullable: false),
                    votes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlternativeTitle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlternativeTitle_Movie_movieId",
                        column: x => x.movieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GenreName = table.Column<string>(nullable: true),
                    MovieId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Genre_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CoverType = table.Column<string>(nullable: true),
                    MovieId = table.Column<int>(nullable: true),
                    URL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlternativeTitle_movieId",
                table: "AlternativeTitle",
                column: "movieId");

            migrationBuilder.CreateIndex(
                name: "IX_Genre_MovieId",
                table: "Genre",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_MovieId",
                table: "Image",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_RatingsId",
                table: "Movie",
                column: "RatingsId");
        }
    }
}
