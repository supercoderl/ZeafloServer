using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeafloServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserLevels_user_id",
                table: "UserLevels");

            migrationBuilder.CreateIndex(
                name: "IX_UserLevels_user_id",
                table: "UserLevels",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserLevels_user_id",
                table: "UserLevels");

            migrationBuilder.CreateIndex(
                name: "IX_UserLevels_user_id",
                table: "UserLevels",
                column: "user_id");
        }
    }
}
