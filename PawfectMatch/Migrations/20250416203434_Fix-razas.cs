using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawfectMatch.Migrations
{
    /// <inheritdoc />
    public partial class Fixrazas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Razas_Categorias_CategoriaId",
                table: "Razas");

            migrationBuilder.RenameColumn(
                name: "IsButtonRighttActive",
                table: "Diapositivas",
                newName: "IsButtonRightActive");

            migrationBuilder.RenameColumn(
                name: "IsButtonLefttActive",
                table: "Diapositivas",
                newName: "IsButtonLeftActive");

            migrationBuilder.AddForeignKey(
                name: "FK_Razas_Categorias_CategoriaId",
                table: "Razas",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Razas_Categorias_CategoriaId",
                table: "Razas");

            migrationBuilder.RenameColumn(
                name: "IsButtonRightActive",
                table: "Diapositivas",
                newName: "IsButtonRighttActive");

            migrationBuilder.RenameColumn(
                name: "IsButtonLeftActive",
                table: "Diapositivas",
                newName: "IsButtonLefttActive");

            migrationBuilder.AddForeignKey(
                name: "FK_Razas_Categorias_CategoriaId",
                table: "Razas",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId");
        }
    }
}
