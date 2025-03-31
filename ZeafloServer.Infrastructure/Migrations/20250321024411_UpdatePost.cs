using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeafloServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Post_MediaType",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "media_type",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "media_url",
                table: "Posts",
                newName: "thumbnail_url");

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "Posts",
                type: "varchar(200)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PostMedia",
                columns: table => new
                {
                    post_media_id = table.Column<Guid>(type: "uuid", nullable: false),
                    post_id = table.Column<Guid>(type: "uuid", nullable: false),
                    media_url = table.Column<string>(type: "text", nullable: false),
                    media_type = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostMedia", x => x.post_media_id);
                    table.CheckConstraint("CK_PostMedia_MediaType", "media_type IN ('Image', 'Video', 'Gif', 'None')");
                    table.ForeignKey(
                        name: "FK_PostMedia_Post_PostId",
                        column: x => x.post_id,
                        principalTable: "Posts",
                        principalColumn: "post_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostReactions",
                columns: table => new
                {
                    post_reaction_id = table.Column<Guid>(type: "uuid", nullable: false),
                    post_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    reaction_type = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostReactions", x => x.post_reaction_id);
                    table.CheckConstraint("CK_PostReaction_ReactionType", "reaction_type IN ('Like', 'Dislike', 'Love', 'Wow', 'Sad', 'Angry')");
                    table.ForeignKey(
                        name: "FK_PostReaction_Post_PostId",
                        column: x => x.post_id,
                        principalTable: "Posts",
                        principalColumn: "post_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostReaction_User_UserId",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostMedia_post_id",
                table: "PostMedia",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "IX_PostReactions_post_id",
                table: "PostReactions",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "IX_PostReactions_user_id",
                table: "PostReactions",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostMedia");

            migrationBuilder.DropTable(
                name: "PostReactions");

            migrationBuilder.DropColumn(
                name: "title",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "thumbnail_url",
                table: "Posts",
                newName: "media_url");

            migrationBuilder.AddColumn<string>(
                name: "media_type",
                table: "Posts",
                type: "text",
                nullable: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Post_MediaType",
                table: "Posts",
                sql: "media_type IN ('Image', 'Video', 'Gif', 'None')");
        }
    }
}
