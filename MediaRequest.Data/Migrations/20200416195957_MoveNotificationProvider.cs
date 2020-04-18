using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaRequest.Data.Migrations
{
    public partial class MoveNotificationProvider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationProvider",
                columns: table => new
                {
                    UserNotificationId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(nullable: true),
                    WebhookURL = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    AvatarUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationProvider", x => x.UserNotificationId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationProvider");
        }
    }
}
