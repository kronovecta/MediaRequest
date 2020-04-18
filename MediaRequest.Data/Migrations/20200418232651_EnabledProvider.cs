using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaRequest.Data.Migrations
{
    public partial class EnabledProvider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "NotificationProvider",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "NotificationProvider");
        }
    }
}
