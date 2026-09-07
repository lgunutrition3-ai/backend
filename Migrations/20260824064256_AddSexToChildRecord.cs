using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutrition_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddSexToChildRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "ChildRecords",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sex",
                table: "ChildRecords");
        }
    }
}

