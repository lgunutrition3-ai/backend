using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutrition_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAnimalDispersalModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AnimalDispersal_BarangayPurokYear",
                table: "AnimalDispersalReports");

            migrationBuilder.DropColumn(
                name: "HouseholdsReceived",
                table: "AnimalDispersalReports");

            migrationBuilder.DropColumn(
                name: "Signature",
                table: "AnimalDispersalReports");

            migrationBuilder.DropColumn(
                name: "TotalHouseholds",
                table: "AnimalDispersalReports");

            migrationBuilder.AddColumn<string>(
                name: "HouseholdName",
                table: "AnimalDispersalReports",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 5, 6, 52, 14, 605, DateTimeKind.Utc).AddTicks(4820));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HouseholdName",
                table: "AnimalDispersalReports");

            migrationBuilder.AddColumn<int>(
                name: "HouseholdsReceived",
                table: "AnimalDispersalReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Signature",
                table: "AnimalDispersalReports",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "TotalHouseholds",
                table: "AnimalDispersalReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 5, 6, 4, 18, 629, DateTimeKind.Utc).AddTicks(1251));

            migrationBuilder.CreateIndex(
                name: "IX_AnimalDispersal_BarangayPurokYear",
                table: "AnimalDispersalReports",
                columns: new[] { "Barangay", "Purok", "Year" },
                unique: true);
        }
    }
}
