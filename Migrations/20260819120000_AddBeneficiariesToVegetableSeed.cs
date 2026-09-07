using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutrition_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddBeneficiariesToVegetableSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Beneficiaries",
                table: "VegetableSeedReports",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 19, 12, 0, 0, 0, DateTimeKind.Utc));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Beneficiaries",
                table: "VegetableSeedReports");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 18, 7, 16, 35, 60, DateTimeKind.Utc).AddTicks(4025));
        }
    }
}