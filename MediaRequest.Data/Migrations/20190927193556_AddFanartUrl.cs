using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaRequest.Data.Migrations
{
    public partial class AddFanartUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FanartUrl",
                table: "MoviePoster",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FanartUrl",
                table: "MoviePoster");
        }
    }
}
