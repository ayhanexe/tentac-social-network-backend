using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class addUserModelToUserFriendsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFriends_AspNetUsers_UserId",
                table: "UserFriends");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserFriends",
                newName: "UserRelationId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFriends_UserId",
                table: "UserFriends",
                newName: "IX_UserFriends_UserRelationId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriends_AspNetUsers_UserRelationId",
                table: "UserFriends",
                column: "UserRelationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFriends_AspNetUsers_UserRelationId",
                table: "UserFriends");

            migrationBuilder.RenameColumn(
                name: "UserRelationId",
                table: "UserFriends",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFriends_UserRelationId",
                table: "UserFriends",
                newName: "IX_UserFriends_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriends_AspNetUsers_UserId",
                table: "UserFriends",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
