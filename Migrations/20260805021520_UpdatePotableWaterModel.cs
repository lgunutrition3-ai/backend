using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutrition_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePotableWaterModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalHouseholds",
                table: "PotableWaterReports");

            migrationBuilder.AddColumn<string>(
                name: "HouseholdName",
                table: "PotableWaterReports",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 5, 2, 15, 19, 942, DateTimeKind.Utc).AddTicks(4815));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HouseholdName",
                table: "PotableWaterReports");

            migrationBuilder.AddColumn<int>(
                name: "TotalHouseholds",
                table: "PotableWaterReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 5, 1, 38, 42, 791, DateTimeKind.Utc).AddTicks(6835));
        }
    }
}
