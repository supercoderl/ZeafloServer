using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeafloServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePlaceEnumV3 : Migration
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
                sql: "type IN ('Restaurant', 'Coffee', 'Hotel', 'HomeStay', 'Resort', 'Market', 'Church', 'Museum', 'Tunnel', 'Zoo', 'Park')");
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
                sql: "type IN ('Restaurant', 'Coffee', 'Hotel', 'HomeStay', 'Resort')");
        }
    }
}
