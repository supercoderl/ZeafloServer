using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeafloServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Plan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    plan_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    monthly_price = table.Column<double>(type: "double precision", nullable: false),
                    annual_price = table.Column<double>(type: "double precision", nullable: false),
                    max_seats = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.plan_id);
                });

            migrationBuilder.CreateTable(
                name: "UserSubscriptions",
                columns: table => new
                {
                    user_subcription_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    plan_id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp", nullable: true),
                    is_trial = table.Column<bool>(type: "boolean", nullable: false),
                    payment_provider_id = table.Column<Guid>(type: "uuid", nullable: true),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSubscriptions", x => x.user_subcription_id);
                    table.ForeignKey(
                        name: "FK_UserSubscription_Plan_PlanId",
                        column: x => x.plan_id,
                        principalTable: "Plans",
                        principalColumn: "plan_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSubscription_User_UserId",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriptions_plan_id",
                table: "UserSubscriptions",
                column: "plan_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriptions_user_id",
                table: "UserSubscriptions",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSubscriptions");

            migrationBuilder.DropTable(
                name: "Plans");
        }
    }
}
