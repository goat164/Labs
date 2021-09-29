using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvancedProgramming_Lesson2.Migrations
{
    public partial class MovieExtraFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSeen",
                table: "Movie",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Movie",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSeen",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Movie");
        }
    }
}
