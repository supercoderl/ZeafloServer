using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeafloServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePlaceEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Place_Type",
                table: "Places");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Place_Type",
                table: "Places",
                sql: "type IN ('Restaurant', 'Coffee', 'Hotel', 'HomeStay', 'Resort')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Place_Type",
                table: "Places");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Place_Type",
                table: "Places",
                sql: "type IN ('Restaurant', 'Hotel', 'HomeStay', 'Resort')");
        }
    }
}
