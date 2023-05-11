using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaiTwitterApi.Migrations
{
    public partial class DataModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TComment",
                columns: table => new
                {
                    CommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatorIdUserId = table.Column<int>(nullable: true),
                    ContentText = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TComment", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_TComment_tUser_CreatorIdUserId",
                        column: x => x.CreatorIdUserId,
                        principalTable: "tUser",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TFollow",
                columns: table => new
                {
                    FollowId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FollowedIdUserId = table.Column<int>(nullable: true),
                    FollowerIdUserId = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TFollow", x => x.FollowId);
                    table.ForeignKey(
                        name: "FK_TFollow_tUser_FollowedIdUserId",
                        column: x => x.FollowedIdUserId,
                        principalTable: "tUser",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TFollow_tUser_FollowerIdUserId",
                        column: x => x.FollowerIdUserId,
                        principalTable: "tUser",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TPost",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatorIdUserId = table.Column<int>(nullable: true),
                    SharedPostIdPostId = table.Column<int>(nullable: true),
                    ContentText = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPost", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_TPost_tUser_CreatorIdUserId",
                        column: x => x.CreatorIdUserId,
                        principalTable: "tUser",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TPost_TPost_SharedPostIdPostId",
                        column: x => x.SharedPostIdPostId,
                        principalTable: "TPost",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TLike",
                columns: table => new
                {
                    LikeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatorIdUserId = table.Column<int>(nullable: true),
                    PostId1 = table.Column<int>(nullable: true),
                    CommentId1 = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TLike", x => x.LikeId);
                    table.ForeignKey(
                        name: "FK_TLike_TComment_CommentId1",
                        column: x => x.CommentId1,
                        principalTable: "TComment",
                        principalColumn: "CommentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TLike_tUser_CreatorIdUserId",
                        column: x => x.CreatorIdUserId,
                        principalTable: "tUser",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TLike_TPost_PostId1",
                        column: x => x.PostId1,
                        principalTable: "TPost",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TComment_CreatorIdUserId",
                table: "TComment",
                column: "CreatorIdUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TFollow_FollowedIdUserId",
                table: "TFollow",
                column: "FollowedIdUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TFollow_FollowerIdUserId",
                table: "TFollow",
                column: "FollowerIdUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TLike_CommentId1",
                table: "TLike",
                column: "CommentId1");

            migrationBuilder.CreateIndex(
                name: "IX_TLike_CreatorIdUserId",
                table: "TLike",
                column: "CreatorIdUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TLike_PostId1",
                table: "TLike",
                column: "PostId1");

            migrationBuilder.CreateIndex(
                name: "IX_TPost_CreatorIdUserId",
                table: "TPost",
                column: "CreatorIdUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TPost_SharedPostIdPostId",
                table: "TPost",
                column: "SharedPostIdPostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TFollow");

            migrationBuilder.DropTable(
                name: "TLike");

            migrationBuilder.DropTable(
                name: "TComment");

            migrationBuilder.DropTable(
                name: "TPost");
        }
    }
}
