using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Data.Migrations
{
    public partial class postimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "posts");
        }
    }
}
