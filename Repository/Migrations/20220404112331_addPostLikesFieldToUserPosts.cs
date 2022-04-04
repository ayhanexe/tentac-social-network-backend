using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class addPostLikesFieldToUserPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserPostsId",
                table: "PostLikes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostLikes_UserPostsId",
                table: "PostLikes",
                column: "UserPostsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostLikes_UserPosts_UserPostsId",
                table: "PostLikes",
                column: "UserPostsId",
                principalTable: "UserPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostLikes_UserPosts_UserPostsId",
                table: "PostLikes");

            migrationBuilder.DropIndex(
                name: "IX_PostLikes_UserPostsId",
                table: "PostLikes");

            migrationBuilder.DropColumn(
                name: "UserPostsId",
                table: "PostLikes");
        }
    }
}
