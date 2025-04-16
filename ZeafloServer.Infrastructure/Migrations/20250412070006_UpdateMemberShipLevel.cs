using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeafloServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMemberShipLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_MemberShipLevel_Type",
                table: "MemberShipLevels");

            migrationBuilder.AddCheckConstraint(
                name: "CK_MemberShipLevel_Type",
                table: "MemberShipLevels",
                sql: "type IN ('Member', 'Silver', 'Gold', 'Diamond')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_MemberShipLevel_Type",
                table: "MemberShipLevels");

            migrationBuilder.AddCheckConstraint(
                name: "CK_MemberShipLevel_Type",
                table: "MemberShipLevels",
                sql: "type IN ('Silver', 'Gold', 'Diamond')");
        }
    }
}
