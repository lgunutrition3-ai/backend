using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutrition_backend.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueIndexGardeningAndCR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CR_BarangayPurokYear",
                table: "CRReports");

            migrationBuilder.DropIndex(
                name: "IX_Gardening_BarangayPurokYear",
                table: "BackyardGardeningReports");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 5, 3, 37, 18, 611, DateTimeKind.Utc).AddTicks(5757));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 5, 3, 30, 15, 354, DateTimeKind.Utc).AddTicks(1246));

            migrationBuilder.CreateIndex(
                name: "IX_CR_BarangayPurokYear",
                table: "CRReports",
                columns: new[] { "Barangay", "Purok", "Year" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gardening_BarangayPurokYear",
                table: "BackyardGardeningReports",
                columns: new[] { "Barangay", "Purok", "Year" },
                unique: true);
        }
    }
}
