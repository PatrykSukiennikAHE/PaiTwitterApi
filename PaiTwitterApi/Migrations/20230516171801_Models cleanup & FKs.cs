using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaiTwitterApi.Migrations
{
    public partial class ModelscleanupFKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TComment_tUser_CreatorIdUserId",
                table: "TComment");

            migrationBuilder.DropForeignKey(
                name: "FK_TFollow_tUser_FollowedIdUserId",
                table: "TFollow");

            migrationBuilder.DropForeignKey(
                name: "FK_TFollow_tUser_FollowerIdUserId",
                table: "TFollow");

            migrationBuilder.DropForeignKey(
                name: "FK_TLike_TComment_CommentId1",
                table: "TLike");

            migrationBuilder.DropForeignKey(
                name: "FK_TLike_tUser_CreatorIdUserId",
                table: "TLike");

            migrationBuilder.DropForeignKey(
                name: "FK_TLike_TPost_PostId1",
                table: "TLike");

            migrationBuilder.DropForeignKey(
                name: "FK_TPost_tUser_CreatorIdUserId",
                table: "TPost");

            migrationBuilder.DropForeignKey(
                name: "FK_TPost_TPost_SharedPostIdPostId",
                table: "TPost");

            migrationBuilder.DropIndex(
                name: "IX_TPost_CreatorIdUserId",
                table: "TPost");

            migrationBuilder.DropIndex(
                name: "IX_TPost_SharedPostIdPostId",
                table: "TPost");

            migrationBuilder.DropIndex(
                name: "IX_TLike_CommentId1",
                table: "TLike");

            migrationBuilder.DropIndex(
                name: "IX_TLike_CreatorIdUserId",
                table: "TLike");

            migrationBuilder.DropIndex(
                name: "IX_TLike_PostId1",
                table: "TLike");

            migrationBuilder.DropIndex(
                name: "IX_TFollow_FollowedIdUserId",
                table: "TFollow");

            migrationBuilder.DropIndex(
                name: "IX_TFollow_FollowerIdUserId",
                table: "TFollow");

            migrationBuilder.DropIndex(
                name: "IX_TComment_CreatorIdUserId",
                table: "TComment");

            migrationBuilder.DropColumn(
                name: "CreatorIdUserId",
                table: "TPost");

            migrationBuilder.DropColumn(
                name: "SharedPostIdPostId",
                table: "TPost");

            migrationBuilder.DropColumn(
                name: "CommentId1",
                table: "TLike");

            migrationBuilder.DropColumn(
                name: "CreatorIdUserId",
                table: "TLike");

            migrationBuilder.DropColumn(
                name: "PostId1",
                table: "TLike");

            migrationBuilder.DropColumn(
                name: "FollowedIdUserId",
                table: "TFollow");

            migrationBuilder.DropColumn(
                name: "FollowerIdUserId",
                table: "TFollow");

            migrationBuilder.DropColumn(
                name: "CreatorIdUserId",
                table: "TComment");

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "TPost",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SharedPostId",
                table: "TPost",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "TLike",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "TLike",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "TLike",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FollowedId",
                table: "TFollow",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FollowerId",
                table: "TFollow",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "TComment",
                nullable: false,
                defaultValue: 0);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TNotification");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "TPost");

            migrationBuilder.DropColumn(
                name: "SharedPostId",
                table: "TPost");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "TLike");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "TLike");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "TLike");

            migrationBuilder.DropColumn(
                name: "FollowedId",
                table: "TFollow");

            migrationBuilder.DropColumn(
                name: "FollowerId",
                table: "TFollow");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "TComment");

            migrationBuilder.AddColumn<int>(
                name: "CreatorIdUserId",
                table: "TPost",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SharedPostIdPostId",
                table: "TPost",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommentId1",
                table: "TLike",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatorIdUserId",
                table: "TLike",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostId1",
                table: "TLike",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FollowedIdUserId",
                table: "TFollow",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FollowerIdUserId",
                table: "TFollow",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatorIdUserId",
                table: "TComment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TPost_CreatorIdUserId",
                table: "TPost",
                column: "CreatorIdUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TPost_SharedPostIdPostId",
                table: "TPost",
                column: "SharedPostIdPostId");

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
                name: "IX_TFollow_FollowedIdUserId",
                table: "TFollow",
                column: "FollowedIdUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TFollow_FollowerIdUserId",
                table: "TFollow",
                column: "FollowerIdUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TComment_CreatorIdUserId",
                table: "TComment",
                column: "CreatorIdUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TComment_tUser_CreatorIdUserId",
                table: "TComment",
                column: "CreatorIdUserId",
                principalTable: "tUser",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TFollow_tUser_FollowedIdUserId",
                table: "TFollow",
                column: "FollowedIdUserId",
                principalTable: "tUser",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TFollow_tUser_FollowerIdUserId",
                table: "TFollow",
                column: "FollowerIdUserId",
                principalTable: "tUser",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TLike_TComment_CommentId1",
                table: "TLike",
                column: "CommentId1",
                principalTable: "TComment",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TLike_tUser_CreatorIdUserId",
                table: "TLike",
                column: "CreatorIdUserId",
                principalTable: "tUser",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TLike_TPost_PostId1",
                table: "TLike",
                column: "PostId1",
                principalTable: "TPost",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TPost_tUser_CreatorIdUserId",
                table: "TPost",
                column: "CreatorIdUserId",
                principalTable: "tUser",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TPost_TPost_SharedPostIdPostId",
                table: "TPost",
                column: "SharedPostIdPostId",
                principalTable: "TPost",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
