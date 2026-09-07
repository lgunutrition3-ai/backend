using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutrition_backend.Migrations
{
    /// <inheritdoc />
    public partial class DropUniqueIndexAnimalRaising : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AnimalRaising_BarangayPurokYear",
                table: "AnimalRaisingReports");

            migrationBuilder.AlterColumn<string>(
                name: "HouseholdName",
                table: "AnimalRaisingReports",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 5, 1, 38, 42, 791, DateTimeKind.Utc).AddTicks(6835));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HouseholdName",
                table: "AnimalRaisingReports",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 5, 1, 32, 37, 800, DateTimeKind.Utc).AddTicks(4952));

            migrationBuilder.CreateIndex(
                name: "IX_AnimalRaising_BarangayPurokYear",
                table: "AnimalRaisingReports",
                columns: new[] { "Barangay", "Purok", "Year" },
                unique: true);
        }
    }
}
