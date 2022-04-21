using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class addRelationToUserStoriesModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "StoryId",
                table: "UserStories",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserStories_StoryId",
                table: "UserStories",
                column: "StoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserStories_Stories_StoryId",
                table: "UserStories",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserStories_Stories_StoryId",
                table: "UserStories");

            migrationBuilder.DropIndex(
                name: "IX_UserStories_StoryId",
                table: "UserStories");

            migrationBuilder.AlterColumn<string>(
                name: "StoryId",
                table: "UserStories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
