using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawfectMatch.Migrations
{
    /// <inheritdoc />
    public partial class Initialaddseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SolicitudesServiciosId",
                table: "SolicitudesServicios",
                newName: "SolicitudServicioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SolicitudServicioId",
                table: "SolicitudesServicios",
                newName: "SolicitudesServiciosId");
        }
    }
}
