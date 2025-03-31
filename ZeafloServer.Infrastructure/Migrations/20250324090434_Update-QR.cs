using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeafloServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "qr_url",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "qr_url",
                table: "Users");
        }
    }
}
