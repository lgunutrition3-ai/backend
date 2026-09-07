using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutrition_backend.Migrations
{
    /// <inheritdoc />
    public partial class RenameHouseholdNamesToHouseholdName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HouseholdNames",
                table: "AnimalRaisingReports",
                newName: "HouseholdName");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 5, 1, 32, 37, 800, DateTimeKind.Utc).AddTicks(4952));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HouseholdName",
                table: "AnimalRaisingReports",
                newName: "HouseholdNames");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 5, 1, 24, 9, 891, DateTimeKind.Utc).AddTicks(3666));
        }
    }
}
