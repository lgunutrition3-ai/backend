using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutrition_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddOtherAnimalsToDispersalAndRaising : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OtherFemale",
                table: "AnimalRaisingReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherMale",
                table: "AnimalRaisingReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherFemale",
                table: "AnimalDispersalReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherMale",
                table: "AnimalDispersalReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 18, 7, 16, 35, 60, DateTimeKind.Utc).AddTicks(4025));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtherFemale",
                table: "AnimalRaisingReports");

            migrationBuilder.DropColumn(
                name: "OtherMale",
                table: "AnimalRaisingReports");

            migrationBuilder.DropColumn(
                name: "OtherFemale",
                table: "AnimalDispersalReports");

            migrationBuilder.DropColumn(
                name: "OtherMale",
                table: "AnimalDispersalReports");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 18, 3, 5, 18, 644, DateTimeKind.Utc).AddTicks(8395));
        }
    }
}
