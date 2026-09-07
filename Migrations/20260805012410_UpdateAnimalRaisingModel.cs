using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutrition_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAnimalRaisingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Signature",
                table: "AnimalRaisingReports");

            migrationBuilder.DropColumn(
                name: "TotalHouseholds",
                table: "AnimalRaisingReports");

            migrationBuilder.AddColumn<string>(
                name: "HouseholdNames",
                table: "AnimalRaisingReports",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 5, 1, 24, 9, 891, DateTimeKind.Utc).AddTicks(3666));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HouseholdNames",
                table: "AnimalRaisingReports");

            migrationBuilder.AddColumn<string>(
                name: "Signature",
                table: "AnimalRaisingReports",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "TotalHouseholds",
                table: "AnimalRaisingReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 3, 6, 56, 44, 337, DateTimeKind.Utc).AddTicks(8723));
        }
    }
}
