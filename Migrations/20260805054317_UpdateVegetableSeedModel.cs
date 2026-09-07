using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutrition_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVegetableSeedModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VegetableSeed_BarangayPurokYear",
                table: "VegetableSeedReports");

            migrationBuilder.DropColumn(
                name: "PoorFamiliesGivenSeeds",
                table: "VegetableSeedReports");

            migrationBuilder.DropColumn(
                name: "SeedCount1",
                table: "VegetableSeedReports");

            migrationBuilder.DropColumn(
                name: "SeedCount2",
                table: "VegetableSeedReports");

            migrationBuilder.DropColumn(
                name: "SeedCount3",
                table: "VegetableSeedReports");

            migrationBuilder.DropColumn(
                name: "SeedType1",
                table: "VegetableSeedReports");

            migrationBuilder.DropColumn(
                name: "SeedType2",
                table: "VegetableSeedReports");

            migrationBuilder.DropColumn(
                name: "SeedType3",
                table: "VegetableSeedReports");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "VegetableSeedReports");

            migrationBuilder.DropColumn(
                name: "TotalHouseholds",
                table: "VegetableSeedReports");

            migrationBuilder.AddColumn<string>(
                name: "HouseholdName",
                table: "VegetableSeedReports",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SeedTypes",
                table: "VegetableSeedReports",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 5, 5, 43, 17, 145, DateTimeKind.Utc).AddTicks(4673));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HouseholdName",
                table: "VegetableSeedReports");

            migrationBuilder.DropColumn(
                name: "SeedTypes",
                table: "VegetableSeedReports");

            migrationBuilder.AddColumn<int>(
                name: "PoorFamiliesGivenSeeds",
                table: "VegetableSeedReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SeedCount1",
                table: "VegetableSeedReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SeedCount2",
                table: "VegetableSeedReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SeedCount3",
                table: "VegetableSeedReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SeedType1",
                table: "VegetableSeedReports",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SeedType2",
                table: "VegetableSeedReports",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SeedType3",
                table: "VegetableSeedReports",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "SubTotal",
                table: "VegetableSeedReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalHouseholds",
                table: "VegetableSeedReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 5, 5, 24, 27, 150, DateTimeKind.Utc).AddTicks(7399));

            migrationBuilder.CreateIndex(
                name: "IX_VegetableSeed_BarangayPurokYear",
                table: "VegetableSeedReports",
                columns: new[] { "Barangay", "Purok", "Year" },
                unique: true);
        }
    }
}
