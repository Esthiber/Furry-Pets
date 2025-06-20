using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawfectMatch.Migrations
{
    /// <inheritdoc />
    public partial class LastCheckfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Presentaciones",
                keyColumn: "PresentacionId",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Presentaciones",
                keyColumn: "PresentacionId",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2025, 4, 21, 19, 12, 23, 649, DateTimeKind.Local).AddTicks(8047));
        }
    }
}
