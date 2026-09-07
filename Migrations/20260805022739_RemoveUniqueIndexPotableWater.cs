using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutrition_backend.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueIndexPotableWater : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PotableWater_BarangayPurokYear",
                table: "PotableWaterReports");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 5, 2, 27, 38, 166, DateTimeKind.Utc).AddTicks(8647));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 5, 2, 15, 19, 942, DateTimeKind.Utc).AddTicks(4815));

            migrationBuilder.CreateIndex(
                name: "IX_PotableWater_BarangayPurokYear",
                table: "PotableWaterReports",
                columns: new[] { "Barangay", "Purok", "Year" },
                unique: true);
        }
    }
}
