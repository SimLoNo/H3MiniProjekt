using Microsoft.EntityFrameworkCore.Migrations;

namespace H3MiniProjekt.DAL.Migrations
{
    public partial class updatedBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublishYear",
                table: "Book",
                newName: "ReleaseYear");

            migrationBuilder.AddColumn<bool>(
                name: "Binding",
                table: "Book",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Pages",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "WordCound",
                table: "Book",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Binding",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "Pages",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "WordCound",
                table: "Book");

            migrationBuilder.RenameColumn(
                name: "ReleaseYear",
                table: "Book",
                newName: "PublishYear");
        }
    }
}
