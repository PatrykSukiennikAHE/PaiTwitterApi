using Microsoft.EntityFrameworkCore.Migrations;

namespace PaiTwitterApi.Migrations
{
    public partial class PostIdfortComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "TComment",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostId",
                table: "TComment");
        }
    }
}
