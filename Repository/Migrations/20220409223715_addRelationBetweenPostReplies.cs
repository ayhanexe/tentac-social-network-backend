using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class addRelationBetweenPostReplies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostLikes_PostReplies_PostRepliesId",
                table: "PostLikes");

            migrationBuilder.RenameColumn(
                name: "PostRepliesId",
                table: "PostLikes",
                newName: "PostReplyId");

            migrationBuilder.RenameIndex(
                name: "IX_PostLikes_PostRepliesId",
                table: "PostLikes",
                newName: "IX_PostLikes_PostReplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostLikes_PostReplies_PostReplyId",
                table: "PostLikes",
                column: "PostReplyId",
                principalTable: "PostReplies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostLikes_PostReplies_PostReplyId",
                table: "PostLikes");

            migrationBuilder.RenameColumn(
                name: "PostReplyId",
                table: "PostLikes",
                newName: "PostRepliesId");

            migrationBuilder.RenameIndex(
                name: "IX_PostLikes_PostReplyId",
                table: "PostLikes",
                newName: "IX_PostLikes_PostRepliesId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostLikes_PostReplies_PostRepliesId",
                table: "PostLikes",
                column: "PostRepliesId",
                principalTable: "PostReplies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
