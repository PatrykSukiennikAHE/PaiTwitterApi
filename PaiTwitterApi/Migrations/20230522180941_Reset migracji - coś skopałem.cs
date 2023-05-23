using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaiTwitterApi.Migrations
{
    public partial class Resetmigracjicośskopałem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TComment",
                columns: table => new
                {
                    CommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(nullable: false),
                    CreatorId = table.Column<int>(nullable: false),
                    ContentText = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TComment", x => x.CommentId);
                });

            migrationBuilder.CreateTable(
                name: "TLike",
                columns: table => new
                {
                    LikeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatorId = table.Column<int>(nullable: false),
                    PostId = table.Column<int>(nullable: false),
                    CommentId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TLike", x => x.LikeId);
                });

            migrationBuilder.CreateTable(
                name: "TNotification",
                columns: table => new
                {
                    NotificationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    ReadDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TNotification", x => x.NotificationId);
                });

            migrationBuilder.CreateTable(
                name: "TPost",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatorId = table.Column<int>(nullable: false),
                    SharedPostId = table.Column<int>(nullable: false),
                    ContentText = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPost", x => x.PostId);
                });

            migrationBuilder.CreateTable(
                name: "tUser",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    LastName = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    UserName = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    Email = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastActivity = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tUser__1788CC4C65F92F39", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "TFollow",
                columns: table => new
                {
                    FollowId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FollowedId = table.Column<int>(nullable: false),
                    FollowerId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TFollow", x => x.FollowId);
                    table.ForeignKey(
                        name: "FK_TFollow_tUser_FollowedId",
                        column: x => x.FollowedId,
                        principalTable: "tUser",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TFollow_tUser_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "tUser",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TFollow_FollowedId",
                table: "TFollow",
                column: "FollowedId");

            migrationBuilder.CreateIndex(
                name: "IX_TFollow_FollowerId",
                table: "TFollow",
                column: "FollowerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TComment");

            migrationBuilder.DropTable(
                name: "TFollow");

            migrationBuilder.DropTable(
                name: "TLike");

            migrationBuilder.DropTable(
                name: "TNotification");

            migrationBuilder.DropTable(
                name: "TPost");

            migrationBuilder.DropTable(
                name: "tUser");
        }
    }
}
