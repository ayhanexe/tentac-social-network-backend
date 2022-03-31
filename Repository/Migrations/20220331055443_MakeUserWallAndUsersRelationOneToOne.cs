using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class MakeUserWallAndUsersRelationOneToOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserWalls_UserId",
                table: "UserWalls");

            migrationBuilder.CreateIndex(
                name: "IX_UserWalls_UserId",
                table: "UserWalls",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserWalls_UserId",
                table: "UserWalls");

            migrationBuilder.CreateIndex(
                name: "IX_UserWalls_UserId",
                table: "UserWalls",
                column: "UserId");
        }
    }
}
