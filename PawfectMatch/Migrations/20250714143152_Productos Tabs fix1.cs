using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawfectMatch.Migrations
{
    /// <inheritdoc />
    public partial class ProductosTabsfix1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProductosInTabs",
                keyColumn: "ProductosInTabsID",
                keyValue: 1,
                column: "Orden",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ProductosInTabs",
                keyColumn: "ProductosInTabsID",
                keyValue: 2,
                column: "Orden",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ProductosInTabs",
                keyColumn: "ProductosInTabsID",
                keyValue: 3,
                column: "Orden",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ProductosInTabs",
                keyColumn: "ProductosInTabsID",
                keyValue: 4,
                column: "Orden",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ProductosInTabs",
                keyColumn: "ProductosInTabsID",
                keyValue: 5,
                column: "Orden",
                value: 5);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
