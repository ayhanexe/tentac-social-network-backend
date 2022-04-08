using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class addPostLikesToPostReplies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostRepliesId",
                table: "PostLikes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostLikes_PostRepliesId",
                table: "PostLikes",
                column: "PostRepliesId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostLikes_PostReplies_PostRepliesId",
                table: "PostLikes",
                column: "PostRepliesId",
                principalTable: "PostReplies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostLikes_PostReplies_PostRepliesId",
                table: "PostLikes");

            migrationBuilder.DropIndex(
                name: "IX_PostLikes_PostRepliesId",
                table: "PostLikes");

            migrationBuilder.DropColumn(
                name: "PostRepliesId",
                table: "PostLikes");
        }
    }
}
