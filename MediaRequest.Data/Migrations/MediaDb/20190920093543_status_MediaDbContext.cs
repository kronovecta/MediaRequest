using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaRequest.Data.Migrations.MediaDb
{
    public partial class status_MediaDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Request",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Request");
        }
    }
}
