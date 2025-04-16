using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeafloServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePlaceV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Places",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "Places");
        }
    }
}
