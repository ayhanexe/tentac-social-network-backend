using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class AddFriendsFieldToUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFriends_AspNetUsers_FriendId",
                table: "UserFriends");

            migrationBuilder.DropIndex(
                name: "IX_UserFriends_FriendId",
                table: "UserFriends");

            migrationBuilder.DropColumn(
                name: "FriendId",
                table: "UserFriends");

            migrationBuilder.AddColumn<string>(
                name: "Friend",
                table: "UserFriends",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "UserFriends",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Friend",
                table: "UserFriends");

            migrationBuilder.DropColumn(
                name: "User",
                table: "UserFriends");

            migrationBuilder.AddColumn<string>(
                name: "FriendId",
                table: "UserFriends",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserFriends_FriendId",
                table: "UserFriends",
                column: "FriendId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriends_AspNetUsers_FriendId",
                table: "UserFriends",
                column: "FriendId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
