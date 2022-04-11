using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class addPostRepliesFieldToUserPostsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserPostsId",
                table: "PostReplies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostReplies_UserPostsId",
                table: "PostReplies",
                column: "UserPostsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostReplies_UserPosts_UserPostsId",
                table: "PostReplies",
                column: "UserPostsId",
                principalTable: "UserPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostReplies_UserPosts_UserPostsId",
                table: "PostReplies");

            migrationBuilder.DropIndex(
                name: "IX_PostReplies_UserPostsId",
                table: "PostReplies");

            migrationBuilder.DropColumn(
                name: "UserPostsId",
                table: "PostReplies");
        }
    }
}
