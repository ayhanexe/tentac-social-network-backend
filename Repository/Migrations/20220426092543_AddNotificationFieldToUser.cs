using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class AddNotificationFieldToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNotification_AspNetUsers_UserId",
                table: "UserNotification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserNotification",
                table: "UserNotification");

            migrationBuilder.RenameTable(
                name: "UserNotification",
                newName: "Notifications");

            migrationBuilder.RenameIndex(
                name: "IX_UserNotification_UserId",
                table: "Notifications",
                newName: "IX_Notifications_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "UserNotification");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_UserId",
                table: "UserNotification",
                newName: "IX_UserNotification_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserNotification",
                table: "UserNotification",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotification_AspNetUsers_UserId",
                table: "UserNotification",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
