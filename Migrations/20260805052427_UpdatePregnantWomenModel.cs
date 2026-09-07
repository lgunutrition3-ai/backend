using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutrition_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePregnantWomenModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HighBMI",
                table: "PregnantWomenReports");

            migrationBuilder.DropColumn(
                name: "LowBMI",
                table: "PregnantWomenReports");

            migrationBuilder.DropColumn(
                name: "NormalBMI",
                table: "PregnantWomenReports");

            migrationBuilder.AddColumn<decimal>(
                name: "BMI",
                table: "PregnantWomenReports",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "BMICategory",
                table: "PregnantWomenReports",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "Height",
                table: "PregnantWomenReports",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "PregnantWomenReports",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "WomanName",
                table: "PregnantWomenReports",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 5, 5, 24, 27, 150, DateTimeKind.Utc).AddTicks(7399));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BMI",
                table: "PregnantWomenReports");

            migrationBuilder.DropColumn(
                name: "BMICategory",
                table: "PregnantWomenReports");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "PregnantWomenReports");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "PregnantWomenReports");

            migrationBuilder.DropColumn(
                name: "WomanName",
                table: "PregnantWomenReports");

            migrationBuilder.AddColumn<int>(
                name: "HighBMI",
                table: "PregnantWomenReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LowBMI",
                table: "PregnantWomenReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NormalBMI",
                table: "PregnantWomenReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 5, 3, 37, 18, 611, DateTimeKind.Utc).AddTicks(5757));
        }
    }
}
