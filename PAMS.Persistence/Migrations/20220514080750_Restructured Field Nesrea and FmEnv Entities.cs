using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PAMS.Persistence.Migrations
{
    public partial class RestructuredFieldNesreaandFmEnvEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CO2TestFMs");

            migrationBuilder.DropTable(
                name: "CO2Tests");

            migrationBuilder.DropTable(
                name: "COMBTestFMs");

            migrationBuilder.DropTable(
                name: "COMBTests");

            migrationBuilder.DropTable(
                name: "COTestFMs");

            migrationBuilder.DropTable(
                name: "COTests");

            migrationBuilder.DropTable(
                name: "H2STestFMs");

            migrationBuilder.DropTable(
                name: "H2STests");

            migrationBuilder.DropTable(
                name: "HMTestFMs");

            migrationBuilder.DropTable(
                name: "HMTests");

            migrationBuilder.DropTable(
                name: "NO2TestFMs");

            migrationBuilder.DropTable(
                name: "NO2Tests");

            migrationBuilder.DropTable(
                name: "NoiseTestFMs");

            migrationBuilder.DropTable(
                name: "NoiseTests");

            migrationBuilder.DropTable(
                name: "O2TestFMs");

            migrationBuilder.DropTable(
                name: "O2Tests");

            migrationBuilder.DropTable(
                name: "PM5TestFMs");

            migrationBuilder.DropTable(
                name: "PM5Tests");

            migrationBuilder.DropTable(
                name: "PMTestFMs");

            migrationBuilder.DropTable(
                name: "PMTests");

            migrationBuilder.DropTable(
                name: "SO2TestFMs");

            migrationBuilder.DropTable(
                name: "SO2Tests");

            migrationBuilder.DropTable(
                name: "TempTestFMs");

            migrationBuilder.DropTable(
                name: "TempTests");

            migrationBuilder.DropTable(
                name: "VOCTestFMs");

            migrationBuilder.DropTable(
                name: "VOCTests");

            migrationBuilder.CreateTable(
                name: "FMENVFieldResult",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FMENVFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FMENVFieldResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FMENVFieldResult_FMENVFields_FMENVFieldId",
                        column: x => x.FMENVFieldId,
                        principalTable: "FMENVFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FMENVTemplate",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FMENVTemplate", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NESREAFieldResult",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NESREAFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NESREAFieldResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NESREAFieldResult_NESREAFields_NESREAFieldId",
                        column: x => x.NESREAFieldId,
                        principalTable: "NESREAFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NESREATemplate",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NESREATemplate", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FMENVFieldResult_FMENVFieldId",
                table: "FMENVFieldResult",
                column: "FMENVFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_NESREAFieldResult_NESREAFieldId",
                table: "NESREAFieldResult",
                column: "NESREAFieldId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FMENVFieldResult");

            migrationBuilder.DropTable(
                name: "FMENVTemplate");

            migrationBuilder.DropTable(
                name: "NESREAFieldResult");

            migrationBuilder.DropTable(
                name: "NESREATemplate");

            migrationBuilder.CreateTable(
                name: "CO2TestFMs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FMENVFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "CO2Tests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NESREAFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "COMBTestFMs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FMENVFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "COMBTests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NESREAFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "COTestFMs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FMENVFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "COTests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NESREAFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "H2STestFMs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FMENVFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "H2STests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NESREAFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "HMTestFMs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FMENVFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "HMTests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NESREAFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "NO2TestFMs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FMENVFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "NO2Tests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NESREAFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "NoiseTestFMs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FMENVFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "NoiseTests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NESREAFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "O2TestFMs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FMENVFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "O2Tests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NESREAFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "PM5TestFMs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FMENVFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "PM5Tests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NESREAFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "PMTestFMs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FMENVFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "PMTests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NESREAFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "SO2TestFMs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FMENVFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "SO2Tests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NESREAFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "TempTestFMs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FMENVFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "TempTests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NESREAFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "VOCTestFMs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FMENVFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "VOCTests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NESREAFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "IX_CO2TestFMs_FMENVFieldId",
                table: "CO2TestFMs",
                column: "FMENVFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CO2Tests_NESREAFieldId",
                table: "CO2Tests",
                column: "NESREAFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_COMBTestFMs_FMENVFieldId",
                table: "COMBTestFMs",
                column: "FMENVFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_COMBTests_NESREAFieldId",
                table: "COMBTests",
                column: "NESREAFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_COTestFMs_FMENVFieldId",
                table: "COTestFMs",
                column: "FMENVFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_COTests_NESREAFieldId",
                table: "COTests",
                column: "NESREAFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_H2STestFMs_FMENVFieldId",
                table: "H2STestFMs",
                column: "FMENVFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_H2STests_NESREAFieldId",
                table: "H2STests",
                column: "NESREAFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HMTestFMs_FMENVFieldId",
                table: "HMTestFMs",
                column: "FMENVFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HMTests_NESREAFieldId",
                table: "HMTests",
                column: "NESREAFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NO2TestFMs_FMENVFieldId",
                table: "NO2TestFMs",
                column: "FMENVFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NO2Tests_NESREAFieldId",
                table: "NO2Tests",
                column: "NESREAFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NoiseTestFMs_FMENVFieldId",
                table: "NoiseTestFMs",
                column: "FMENVFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NoiseTests_NESREAFieldId",
                table: "NoiseTests",
                column: "NESREAFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_O2TestFMs_FMENVFieldId",
                table: "O2TestFMs",
                column: "FMENVFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_O2Tests_NESREAFieldId",
                table: "O2Tests",
                column: "NESREAFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PM5TestFMs_FMENVFieldId",
                table: "PM5TestFMs",
                column: "FMENVFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PM5Tests_NESREAFieldId",
                table: "PM5Tests",
                column: "NESREAFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PMTestFMs_FMENVFieldId",
                table: "PMTestFMs",
                column: "FMENVFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PMTests_NESREAFieldId",
                table: "PMTests",
                column: "NESREAFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SO2TestFMs_FMENVFieldId",
                table: "SO2TestFMs",
                column: "FMENVFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SO2Tests_NESREAFieldId",
                table: "SO2Tests",
                column: "NESREAFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TempTestFMs_FMENVFieldId",
                table: "TempTestFMs",
                column: "FMENVFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TempTests_NESREAFieldId",
                table: "TempTests",
                column: "NESREAFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VOCTestFMs_FMENVFieldId",
                table: "VOCTestFMs",
                column: "FMENVFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VOCTests_NESREAFieldId",
                table: "VOCTests",
                column: "NESREAFieldId",
                unique: true);
        }
    }
}
