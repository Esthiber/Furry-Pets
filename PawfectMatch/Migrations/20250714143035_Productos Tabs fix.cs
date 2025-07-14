using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawfectMatch.Migrations
{
    /// <inheritdoc />
    public partial class ProductosTabsfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Orden",
                table: "ProductosInTabs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ProductosInTabs",
                keyColumn: "ProductosInTabsID",
                keyValue: 1,
                column: "Orden",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ProductosInTabs",
                keyColumn: "ProductosInTabsID",
                keyValue: 2,
                column: "Orden",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ProductosInTabs",
                keyColumn: "ProductosInTabsID",
                keyValue: 3,
                column: "Orden",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ProductosInTabs",
                keyColumn: "ProductosInTabsID",
                keyValue: 4,
                column: "Orden",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ProductosInTabs",
                keyColumn: "ProductosInTabsID",
                keyValue: 5,
                column: "Orden",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Orden",
                table: "ProductosInTabs");
        }
    }
}
