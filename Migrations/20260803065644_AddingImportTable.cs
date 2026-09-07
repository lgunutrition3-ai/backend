using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutrition_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddingImportTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimalDispersalReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Barangay = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Purok = table.Column<int>(type: "int", nullable: false),
                    TotalHouseholds = table.Column<int>(type: "int", nullable: false),
                    HouseholdsReceived = table.Column<int>(type: "int", nullable: false),
                    ChickenMale = table.Column<int>(type: "int", nullable: false),
                    ChickenFemale = table.Column<int>(type: "int", nullable: false),
                    PigMale = table.Column<int>(type: "int", nullable: false),
                    PigFemale = table.Column<int>(type: "int", nullable: false),
                    GoatMale = table.Column<int>(type: "int", nullable: false),
                    GoatFemale = table.Column<int>(type: "int", nullable: false),
                    CowMale = table.Column<int>(type: "int", nullable: false),
                    CowFemale = table.Column<int>(type: "int", nullable: false),
                    CarabaoMale = table.Column<int>(type: "int", nullable: false),
                    CarabaoFemale = table.Column<int>(type: "int", nullable: false),
                    Signature = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    RecordedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RecordedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalDispersalReports", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AnimalRaisingReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Barangay = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Purok = table.Column<int>(type: "int", nullable: false),
                    TotalHouseholds = table.Column<int>(type: "int", nullable: false),
                    ChickenMale = table.Column<int>(type: "int", nullable: false),
                    ChickenFemale = table.Column<int>(type: "int", nullable: false),
                    PigMale = table.Column<int>(type: "int", nullable: false),
                    PigFemale = table.Column<int>(type: "int", nullable: false),
                    GoatMale = table.Column<int>(type: "int", nullable: false),
                    GoatFemale = table.Column<int>(type: "int", nullable: false),
                    CowMale = table.Column<int>(type: "int", nullable: false),
                    CowFemale = table.Column<int>(type: "int", nullable: false),
                    CarabaoMale = table.Column<int>(type: "int", nullable: false),
                    CarabaoFemale = table.Column<int>(type: "int", nullable: false),
                    Signature = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    RecordedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RecordedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalRaisingReports", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BackyardGardeningReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Barangay = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Purok = table.Column<int>(type: "int", nullable: false),
                    TotalHouseholds = table.Column<int>(type: "int", nullable: false),
                    WithGarden = table.Column<int>(type: "int", nullable: false),
                    WithoutGarden = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    RecordedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RecordedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackyardGardeningReports", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CRReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Barangay = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Purok = table.Column<int>(type: "int", nullable: false),
                    TotalHouseholds = table.Column<int>(type: "int", nullable: false),
                    WithCR = table.Column<int>(type: "int", nullable: false),
                    WithoutCR = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    RecordedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RecordedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRReports", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IodizedSaltReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Barangay = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Purok = table.Column<int>(type: "int", nullable: false),
                    StoreName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FineSaltFidel = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FineSaltUFC = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FineSaltPacificBay = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FineSaltOthers = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RockSaltAtlantic = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    RockSaltFidel = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    RockSaltLasap = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    RockSaltPagAsa = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    RockSaltJay = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    RockSaltOthers = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OilUFC = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    OilJolly = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    OilOthers = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RecordedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PreparedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NotedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApprovedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IodizedSaltReports", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PotableWaterReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Barangay = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Purok = table.Column<int>(type: "int", nullable: false),
                    TotalHouseholds = table.Column<int>(type: "int", nullable: false),
                    Level1 = table.Column<int>(type: "int", nullable: false),
                    Level2 = table.Column<int>(type: "int", nullable: false),
                    Level3 = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    RecordedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RecordedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PotableWaterReports", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PregnantWomenReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Barangay = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Purok = table.Column<int>(type: "int", nullable: false),
                    HighBMI = table.Column<int>(type: "int", nullable: false),
                    LowBMI = table.Column<int>(type: "int", nullable: false),
                    NormalBMI = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    RecordedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RecordedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PregnantWomenReports", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VegetableSeedReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Barangay = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Purok = table.Column<int>(type: "int", nullable: false),
                    TotalHouseholds = table.Column<int>(type: "int", nullable: false),
                    PoorFamiliesGivenSeeds = table.Column<int>(type: "int", nullable: false),
                    SeedType1 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SeedCount1 = table.Column<int>(type: "int", nullable: false),
                    SeedType2 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SeedCount2 = table.Column<int>(type: "int", nullable: false),
                    SeedType3 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SeedCount3 = table.Column<int>(type: "int", nullable: false),
                    SubTotal = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    RecordedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RecordedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VegetableSeedReports", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 3, 6, 56, 44, 337, DateTimeKind.Utc).AddTicks(8723));

            migrationBuilder.CreateIndex(
                name: "IX_AnimalDispersal_BarangayPurokYear",
                table: "AnimalDispersalReports",
                columns: new[] { "Barangay", "Purok", "Year" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnimalRaising_BarangayPurokYear",
                table: "AnimalRaisingReports",
                columns: new[] { "Barangay", "Purok", "Year" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gardening_BarangayPurokYear",
                table: "BackyardGardeningReports",
                columns: new[] { "Barangay", "Purok", "Year" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CR_BarangayPurokYear",
                table: "CRReports",
                columns: new[] { "Barangay", "Purok", "Year" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IodizedSalt_BarangayPurokStore",
                table: "IodizedSaltReports",
                columns: new[] { "Barangay", "Purok", "StoreName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PotableWater_BarangayPurokYear",
                table: "PotableWaterReports",
                columns: new[] { "Barangay", "Purok", "Year" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PregnantWomen_BarangayPurokYear",
                table: "PregnantWomenReports",
                columns: new[] { "Barangay", "Purok", "Year" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VegetableSeed_BarangayPurokYear",
                table: "VegetableSeedReports",
                columns: new[] { "Barangay", "Purok", "Year" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalDispersalReports");

            migrationBuilder.DropTable(
                name: "AnimalRaisingReports");

            migrationBuilder.DropTable(
                name: "BackyardGardeningReports");

            migrationBuilder.DropTable(
                name: "CRReports");

            migrationBuilder.DropTable(
                name: "IodizedSaltReports");

            migrationBuilder.DropTable(
                name: "PotableWaterReports");

            migrationBuilder.DropTable(
                name: "PregnantWomenReports");

            migrationBuilder.DropTable(
                name: "VegetableSeedReports");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 31, 0, 32, 18, 83, DateTimeKind.Utc).AddTicks(401));
        }
    }
}
