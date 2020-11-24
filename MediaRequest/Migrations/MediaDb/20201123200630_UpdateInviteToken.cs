using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaRequest.WebUI.Migrations.MediaDb
{
    public partial class UpdateInviteToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_InviteTokens_Id",
                table: "InviteTokens",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InviteTokens_Id",
                table: "InviteTokens");
        }
    }
}
