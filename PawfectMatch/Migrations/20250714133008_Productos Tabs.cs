using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PawfectMatch.Migrations
{
    /// <inheritdoc />
    public partial class ProductosTabs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VetasTabs",
                columns: table => new
                {
                    VetasTabsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VetasTabs", x => x.VetasTabsID);
                });

            migrationBuilder.CreateTable(
                name: "ProductosInTabs",
                columns: table => new
                {
                    ProductosInTabsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VetasTabsID = table.Column<int>(type: "int", nullable: false),
                    ProductosID = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosInTabs", x => x.ProductosInTabsID);
                    table.ForeignKey(
                        name: "FK_ProductosInTabs_Productos_ProductosID",
                        column: x => x.ProductosID,
                        principalTable: "Productos",
                        principalColumn: "ProductosID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductosInTabs_VetasTabs_VetasTabsID",
                        column: x => x.VetasTabsID,
                        principalTable: "VetasTabs",
                        principalColumn: "VetasTabsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "VetasTabs",
                columns: new[] { "VetasTabsID", "Color", "Icono", "IsDeleted", "Nombre", "Orden" },
                values: new object[] { 1, "#FF5733", "fas fa-home", false, "Inicio", 1 });

            migrationBuilder.InsertData(
                table: "ProductosInTabs",
                columns: new[] { "ProductosInTabsID", "IsDeleted", "ProductosID", "VetasTabsID" },
                values: new object[,]
                {
                    { 1, false, 1, 1 },
                    { 2, false, 2, 1 },
                    { 3, false, 3, 1 },
                    { 4, false, 4, 1 },
                    { 5, false, 5, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductosInTabs_ProductosID",
                table: "ProductosInTabs",
                column: "ProductosID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosInTabs_VetasTabsID",
                table: "ProductosInTabs",
                column: "VetasTabsID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductosInTabs");

            migrationBuilder.DropTable(
                name: "VetasTabs");
        }
    }
}
