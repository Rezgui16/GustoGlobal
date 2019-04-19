using Microsoft.EntityFrameworkCore.Migrations;

namespace Gusto.Migrations
{
    public partial class ModifRecette : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Titre",
                table: "Recette",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Difficulte",
                table: "Recette",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Titre",
                table: "Recette",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Difficulte",
                table: "Recette",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 9);
        }
    }
}
