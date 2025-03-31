using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeafloServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_online",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_login_time",
                table: "Users",
                type: "timestamp",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_online",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "last_login_time",
                table: "Users");
        }
    }
}
