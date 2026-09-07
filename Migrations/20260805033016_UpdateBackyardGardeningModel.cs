using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutrition_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBackyardGardeningModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalHouseholds",
                table: "BackyardGardeningReports");

            migrationBuilder.DropColumn(
                name: "WithGarden",
                table: "BackyardGardeningReports");

            migrationBuilder.DropColumn(
                name: "WithoutGarden",
                table: "BackyardGardeningReports");

            migrationBuilder.AddColumn<bool>(
                name: "HasGarden",
                table: "BackyardGardeningReports",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "HouseholdName",
                table: "BackyardGardeningReports",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 5, 3, 30, 15, 354, DateTimeKind.Utc).AddTicks(1246));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasGarden",
                table: "BackyardGardeningReports");

            migrationBuilder.DropColumn(
                name: "HouseholdName",
                table: "BackyardGardeningReports");

            migrationBuilder.AddColumn<int>(
                name: "TotalHouseholds",
                table: "BackyardGardeningReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WithGarden",
                table: "BackyardGardeningReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WithoutGarden",
                table: "BackyardGardeningReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 5, 3, 13, 41, 868, DateTimeKind.Utc).AddTicks(8803));
        }
    }
}
