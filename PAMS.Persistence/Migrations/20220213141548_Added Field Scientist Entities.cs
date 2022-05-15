using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PAMS.Persistence.Migrations
{
    public partial class AddedFieldScientistEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SamplePointLocations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SamplePointLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SamplePointLocations_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DPRFields",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SamplePointLocationId = table.Column<long>(type: "bigint", nullable: false),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DPRFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DPRFields_SamplePointLocations_SamplePointLocationId",
                        column: x => x.SamplePointLocationId,
                        principalTable: "SamplePointLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FMENVFields",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SamplePointLocationId = table.Column<long>(type: "bigint", nullable: false),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FMENVFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FMENVFields_SamplePointLocations_SamplePointLocationId",
                        column: x => x.SamplePointLocationId,
                        principalTable: "SamplePointLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NESREAFields",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SamplePointLocationId = table.Column<long>(type: "bigint", nullable: false),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NESREAFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NESREAFields_SamplePointLocations_SamplePointLocationId",
                        column: x => x.SamplePointLocationId,
                        principalTable: "SamplePointLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CO2TestDPRs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DPRFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CO2TestDPRs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CO2TestDPRs_DPRFields_DPRFieldId",
                        column: x => x.DPRFieldId,
                        principalTable: "DPRFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COMBTestDPRs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DPRFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMBTestDPRs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COMBTestDPRs_DPRFields_DPRFieldId",
                        column: x => x.DPRFieldId,
                        principalTable: "DPRFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COTestDPRs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DPRFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COTestDPRs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COTestDPRs_DPRFields_DPRFieldId",
                        column: x => x.DPRFieldId,
                        principalTable: "DPRFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "H2STestDPRs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DPRFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_H2STestDPRs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_H2STestDPRs_DPRFields_DPRFieldId",
                        column: x => x.DPRFieldId,
                        principalTable: "DPRFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HMTestDPRs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DPRFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HMTestDPRs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HMTestDPRs_DPRFields_DPRFieldId",
                        column: x => x.DPRFieldId,
                        principalTable: "DPRFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NO2TestDPRs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DPRFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NO2TestDPRs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NO2TestDPRs_DPRFields_DPRFieldId",
                        column: x => x.DPRFieldId,
                        principalTable: "DPRFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NoiseTestDPRs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DPRFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoiseTestDPRs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NoiseTestDPRs_DPRFields_DPRFieldId",
                        column: x => x.DPRFieldId,
                        principalTable: "DPRFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "O2TestDPRs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DPRFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_O2TestDPRs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_O2TestDPRs_DPRFields_DPRFieldId",
                        column: x => x.DPRFieldId,
                        principalTable: "DPRFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PM5TestDPRs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DPRFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PM5TestDPRs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PM5TestDPRs_DPRFields_DPRFieldId",
                        column: x => x.DPRFieldId,
                        principalTable: "DPRFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PMTestDPRs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DPRFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMTestDPRs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PMTestDPRs_DPRFields_DPRFieldId",
                        column: x => x.DPRFieldId,
                        principalTable: "DPRFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SO2TestDPRs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DPRFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SO2TestDPRs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SO2TestDPRs_DPRFields_DPRFieldId",
                        column: x => x.DPRFieldId,
                        principalTable: "DPRFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TempTestDPRs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DPRFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempTestDPRs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TempTestDPRs_DPRFields_DPRFieldId",
                        column: x => x.DPRFieldId,
                        principalTable: "DPRFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VOCTestDPRs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DPRFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VOCTestDPRs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VOCTestDPRs_DPRFields_DPRFieldId",
                        column: x => x.DPRFieldId,
                        principalTable: "DPRFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CO2TestFMs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FMENVFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CO2TestFMs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CO2TestFMs_FMENVFields_FMENVFieldId",
                        column: x => x.FMENVFieldId,
                        principalTable: "FMENVFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COMBTestFMs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FMENVFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMBTestFMs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COMBTestFMs_FMENVFields_FMENVFieldId",
                        column: x => x.FMENVFieldId,
                        principalTable: "FMENVFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COTestFMs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FMENVFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COTestFMs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COTestFMs_FMENVFields_FMENVFieldId",
                        column: x => x.FMENVFieldId,
                        principalTable: "FMENVFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "H2STestFMs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FMENVFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_H2STestFMs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_H2STestFMs_FMENVFields_FMENVFieldId",
                        column: x => x.FMENVFieldId,
                        principalTable: "FMENVFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HMTestFMs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FMENVFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HMTestFMs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HMTestFMs_FMENVFields_FMENVFieldId",
                        column: x => x.FMENVFieldId,
                        principalTable: "FMENVFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NO2TestFMs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FMENVFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NO2TestFMs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NO2TestFMs_FMENVFields_FMENVFieldId",
                        column: x => x.FMENVFieldId,
                        principalTable: "FMENVFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NoiseTestFMs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FMENVFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoiseTestFMs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NoiseTestFMs_FMENVFields_FMENVFieldId",
                        column: x => x.FMENVFieldId,
                        principalTable: "FMENVFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "O2TestFMs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FMENVFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_O2TestFMs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_O2TestFMs_FMENVFields_FMENVFieldId",
                        column: x => x.FMENVFieldId,
                        principalTable: "FMENVFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PM5TestFMs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FMENVFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PM5TestFMs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PM5TestFMs_FMENVFields_FMENVFieldId",
                        column: x => x.FMENVFieldId,
                        principalTable: "FMENVFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PMTestFMs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FMENVFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMTestFMs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PMTestFMs_FMENVFields_FMENVFieldId",
                        column: x => x.FMENVFieldId,
                        principalTable: "FMENVFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SO2TestFMs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FMENVFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SO2TestFMs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SO2TestFMs_FMENVFields_FMENVFieldId",
                        column: x => x.FMENVFieldId,
                        principalTable: "FMENVFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TempTestFMs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FMENVFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempTestFMs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TempTestFMs_FMENVFields_FMENVFieldId",
                        column: x => x.FMENVFieldId,
                        principalTable: "FMENVFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VOCTestFMs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FMENVFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VOCTestFMs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VOCTestFMs_FMENVFields_FMENVFieldId",
                        column: x => x.FMENVFieldId,
                        principalTable: "FMENVFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CO2Tests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NESREAFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CO2Tests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CO2Tests_NESREAFields_NESREAFieldId",
                        column: x => x.NESREAFieldId,
                        principalTable: "NESREAFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COMBTests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NESREAFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMBTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COMBTests_NESREAFields_NESREAFieldId",
                        column: x => x.NESREAFieldId,
                        principalTable: "NESREAFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COTests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NESREAFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COTests_NESREAFields_NESREAFieldId",
                        column: x => x.NESREAFieldId,
                        principalTable: "NESREAFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FieldLocations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NESREAFieldId = table.Column<long>(type: "bigint", nullable: true),
                    FMENVFieldId = table.Column<long>(type: "bigint", nullable: true),
                    DPRFieldId = table.Column<long>(type: "bigint", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldLocations_DPRFields_DPRFieldId",
                        column: x => x.DPRFieldId,
                        principalTable: "DPRFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FieldLocations_FMENVFields_FMENVFieldId",
                        column: x => x.FMENVFieldId,
                        principalTable: "FMENVFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FieldLocations_NESREAFields_NESREAFieldId",
                        column: x => x.NESREAFieldId,
                        principalTable: "NESREAFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "H2STests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NESREAFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_H2STests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_H2STests_NESREAFields_NESREAFieldId",
                        column: x => x.NESREAFieldId,
                        principalTable: "NESREAFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HMTests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NESREAFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HMTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HMTests_NESREAFields_NESREAFieldId",
                        column: x => x.NESREAFieldId,
                        principalTable: "NESREAFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NO2Tests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NESREAFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NO2Tests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NO2Tests_NESREAFields_NESREAFieldId",
                        column: x => x.NESREAFieldId,
                        principalTable: "NESREAFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NoiseTests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NESREAFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoiseTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NoiseTests_NESREAFields_NESREAFieldId",
                        column: x => x.NESREAFieldId,
                        principalTable: "NESREAFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "O2Tests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NESREAFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_O2Tests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_O2Tests_NESREAFields_NESREAFieldId",
                        column: x => x.NESREAFieldId,
                        principalTable: "NESREAFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PM5Tests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NESREAFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PM5Tests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PM5Tests_NESREAFields_NESREAFieldId",
                        column: x => x.NESREAFieldId,
                        principalTable: "NESREAFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PMTests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NESREAFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PMTests_NESREAFields_NESREAFieldId",
                        column: x => x.NESREAFieldId,
                        principalTable: "NESREAFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SO2Tests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NESREAFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SO2Tests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SO2Tests_NESREAFields_NESREAFieldId",
                        column: x => x.NESREAFieldId,
                        principalTable: "NESREAFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TempTests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NESREAFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TempTests_NESREAFields_NESREAFieldId",
                        column: x => x.NESREAFieldId,
                        principalTable: "NESREAFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VOCTests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NESREAFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestResult = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VOCTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VOCTests_NESREAFields_NESREAFieldId",
                        column: x => x.NESREAFieldId,
                        principalTable: "NESREAFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CO2TestDPRs_DPRFieldId",
                table: "CO2TestDPRs",
                column: "DPRFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_CO2TestFMs_FMENVFieldId",
                table: "CO2TestFMs",
                column: "FMENVFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_CO2Tests_NESREAFieldId",
                table: "CO2Tests",
                column: "NESREAFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_COMBTestDPRs_DPRFieldId",
                table: "COMBTestDPRs",
                column: "DPRFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_COMBTestFMs_FMENVFieldId",
                table: "COMBTestFMs",
                column: "FMENVFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_COMBTests_NESREAFieldId",
                table: "COMBTests",
                column: "NESREAFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_COTestDPRs_DPRFieldId",
                table: "COTestDPRs",
                column: "DPRFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_COTestFMs_FMENVFieldId",
                table: "COTestFMs",
                column: "FMENVFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_COTests_NESREAFieldId",
                table: "COTests",
                column: "NESREAFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_DPRFields_SamplePointLocationId",
                table: "DPRFields",
                column: "SamplePointLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldLocations_DPRFieldId",
                table: "FieldLocations",
                column: "DPRFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldLocations_FMENVFieldId",
                table: "FieldLocations",
                column: "FMENVFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldLocations_NESREAFieldId",
                table: "FieldLocations",
                column: "NESREAFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_FMENVFields_SamplePointLocationId",
                table: "FMENVFields",
                column: "SamplePointLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_H2STestDPRs_DPRFieldId",
                table: "H2STestDPRs",
                column: "DPRFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_H2STestFMs_FMENVFieldId",
                table: "H2STestFMs",
                column: "FMENVFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_H2STests_NESREAFieldId",
                table: "H2STests",
                column: "NESREAFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_HMTestDPRs_DPRFieldId",
                table: "HMTestDPRs",
                column: "DPRFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_HMTestFMs_FMENVFieldId",
                table: "HMTestFMs",
                column: "FMENVFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_HMTests_NESREAFieldId",
                table: "HMTests",
                column: "NESREAFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_NESREAFields_SamplePointLocationId",
                table: "NESREAFields",
                column: "SamplePointLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_NO2TestDPRs_DPRFieldId",
                table: "NO2TestDPRs",
                column: "DPRFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_NO2TestFMs_FMENVFieldId",
                table: "NO2TestFMs",
                column: "FMENVFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_NO2Tests_NESREAFieldId",
                table: "NO2Tests",
                column: "NESREAFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_NoiseTestDPRs_DPRFieldId",
                table: "NoiseTestDPRs",
                column: "DPRFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_NoiseTestFMs_FMENVFieldId",
                table: "NoiseTestFMs",
                column: "FMENVFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_NoiseTests_NESREAFieldId",
                table: "NoiseTests",
                column: "NESREAFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_O2TestDPRs_DPRFieldId",
                table: "O2TestDPRs",
                column: "DPRFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_O2TestFMs_FMENVFieldId",
                table: "O2TestFMs",
                column: "FMENVFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_O2Tests_NESREAFieldId",
                table: "O2Tests",
                column: "NESREAFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_PM5TestDPRs_DPRFieldId",
                table: "PM5TestDPRs",
                column: "DPRFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_PM5TestFMs_FMENVFieldId",
                table: "PM5TestFMs",
                column: "FMENVFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_PM5Tests_NESREAFieldId",
                table: "PM5Tests",
                column: "NESREAFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_PMTestDPRs_DPRFieldId",
                table: "PMTestDPRs",
                column: "DPRFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_PMTestFMs_FMENVFieldId",
                table: "PMTestFMs",
                column: "FMENVFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_PMTests_NESREAFieldId",
                table: "PMTests",
                column: "NESREAFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_SamplePointLocations_ClientID",
                table: "SamplePointLocations",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_SO2TestDPRs_DPRFieldId",
                table: "SO2TestDPRs",
                column: "DPRFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_SO2TestFMs_FMENVFieldId",
                table: "SO2TestFMs",
                column: "FMENVFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_SO2Tests_NESREAFieldId",
                table: "SO2Tests",
                column: "NESREAFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_TempTestDPRs_DPRFieldId",
                table: "TempTestDPRs",
                column: "DPRFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_TempTestFMs_FMENVFieldId",
                table: "TempTestFMs",
                column: "FMENVFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_TempTests_NESREAFieldId",
                table: "TempTests",
                column: "NESREAFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_VOCTestDPRs_DPRFieldId",
                table: "VOCTestDPRs",
                column: "DPRFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_VOCTestFMs_FMENVFieldId",
                table: "VOCTestFMs",
                column: "FMENVFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_VOCTests_NESREAFieldId",
                table: "VOCTests",
                column: "NESREAFieldId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CO2TestDPRs");

            migrationBuilder.DropTable(
                name: "CO2TestFMs");

            migrationBuilder.DropTable(
                name: "CO2Tests");

            migrationBuilder.DropTable(
                name: "COMBTestDPRs");

            migrationBuilder.DropTable(
                name: "COMBTestFMs");

            migrationBuilder.DropTable(
                name: "COMBTests");

            migrationBuilder.DropTable(
                name: "COTestDPRs");

            migrationBuilder.DropTable(
                name: "COTestFMs");

            migrationBuilder.DropTable(
                name: "COTests");

            migrationBuilder.DropTable(
                name: "FieldLocations");

            migrationBuilder.DropTable(
                name: "H2STestDPRs");

            migrationBuilder.DropTable(
                name: "H2STestFMs");

            migrationBuilder.DropTable(
                name: "H2STests");

            migrationBuilder.DropTable(
                name: "HMTestDPRs");

            migrationBuilder.DropTable(
                name: "HMTestFMs");

            migrationBuilder.DropTable(
                name: "HMTests");

            migrationBuilder.DropTable(
                name: "NO2TestDPRs");

            migrationBuilder.DropTable(
                name: "NO2TestFMs");

            migrationBuilder.DropTable(
                name: "NO2Tests");

            migrationBuilder.DropTable(
                name: "NoiseTestDPRs");

            migrationBuilder.DropTable(
                name: "NoiseTestFMs");

            migrationBuilder.DropTable(
                name: "NoiseTests");

            migrationBuilder.DropTable(
                name: "O2TestDPRs");

            migrationBuilder.DropTable(
                name: "O2TestFMs");

            migrationBuilder.DropTable(
                name: "O2Tests");

            migrationBuilder.DropTable(
                name: "PM5TestDPRs");

            migrationBuilder.DropTable(
                name: "PM5TestFMs");

            migrationBuilder.DropTable(
                name: "PM5Tests");

            migrationBuilder.DropTable(
                name: "PMTestDPRs");

            migrationBuilder.DropTable(
                name: "PMTestFMs");

            migrationBuilder.DropTable(
                name: "PMTests");

            migrationBuilder.DropTable(
                name: "SO2TestDPRs");

            migrationBuilder.DropTable(
                name: "SO2TestFMs");

            migrationBuilder.DropTable(
                name: "SO2Tests");

            migrationBuilder.DropTable(
                name: "TempTestDPRs");

            migrationBuilder.DropTable(
                name: "TempTestFMs");

            migrationBuilder.DropTable(
                name: "TempTests");

            migrationBuilder.DropTable(
                name: "VOCTestDPRs");

            migrationBuilder.DropTable(
                name: "VOCTestFMs");

            migrationBuilder.DropTable(
                name: "VOCTests");

            migrationBuilder.DropTable(
                name: "DPRFields");

            migrationBuilder.DropTable(
                name: "FMENVFields");

            migrationBuilder.DropTable(
                name: "NESREAFields");

            migrationBuilder.DropTable(
                name: "SamplePointLocations");
        }
    }
}
