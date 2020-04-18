using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaRequest.Data.Migrations
{
    public partial class ProviderType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProviderType",
                table: "NotificationProvider",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProviderType",
                table: "NotificationProvider");
        }
    }
}
