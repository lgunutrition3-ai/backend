using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutrition_backend.Migrations
{
    /// <inheritdoc />
    public partial class MakeSeedAdminSuperAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2026, 9, 2, 2, 10, 6, 835, DateTimeKind.Utc).AddTicks(3664), "superadmin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2026, 8, 24, 7, 19, 39, 767, DateTimeKind.Utc).AddTicks(5295), "admin" });
        }
    }
}
