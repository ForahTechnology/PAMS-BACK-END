using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PAMS.Persistence.Migrations
{
    public partial class ChangeinDprEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CO2TestDPRs");

            migrationBuilder.DropTable(
                name: "COMBTestDPRs");

            migrationBuilder.DropTable(
                name: "COTestDPRs");

            migrationBuilder.DropTable(
                name: "H2STestDPRs");

            migrationBuilder.DropTable(
                name: "HMTestDPRs");

            migrationBuilder.DropTable(
                name: "NO2TestDPRs");

            migrationBuilder.DropTable(
                name: "NoiseTestDPRs");

            migrationBuilder.DropTable(
                name: "O2TestDPRs");

            migrationBuilder.DropTable(
                name: "PM5TestDPRs");

            migrationBuilder.DropTable(
                name: "PMTestDPRs");

            migrationBuilder.DropTable(
                name: "SO2TestDPRs");

            migrationBuilder.DropTable(
                name: "TempTestDPRs");

            migrationBuilder.DropTable(
                name: "VOCTestDPRs");

            migrationBuilder.CreateTable(
                name: "DPRFieldResult",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DPRFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DPRFieldResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DPRFieldResult_DPRFields_DPRFieldId",
                        column: x => x.DPRFieldId,
                        principalTable: "DPRFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DPRTemplate",
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
                    table.PrimaryKey("PK_DPRTemplate", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DPRFieldResult_DPRFieldId",
                table: "DPRFieldResult",
                column: "DPRFieldId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DPRFieldResult");

            migrationBuilder.DropTable(
                name: "DPRTemplate");

            migrationBuilder.CreateTable(
                name: "CO2TestDPRs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DPRFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    DPRFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    DPRFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    DPRFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    DPRFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    DPRFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    DPRFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    DPRFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    DPRFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    DPRFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    DPRFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    DPRFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    DPRFieldId = table.Column<long>(type: "bigint", nullable: false),
                    TestLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_CO2TestDPRs_DPRFieldId",
                table: "CO2TestDPRs",
                column: "DPRFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_COMBTestDPRs_DPRFieldId",
                table: "COMBTestDPRs",
                column: "DPRFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_COTestDPRs_DPRFieldId",
                table: "COTestDPRs",
                column: "DPRFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_H2STestDPRs_DPRFieldId",
                table: "H2STestDPRs",
                column: "DPRFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HMTestDPRs_DPRFieldId",
                table: "HMTestDPRs",
                column: "DPRFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NO2TestDPRs_DPRFieldId",
                table: "NO2TestDPRs",
                column: "DPRFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NoiseTestDPRs_DPRFieldId",
                table: "NoiseTestDPRs",
                column: "DPRFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_O2TestDPRs_DPRFieldId",
                table: "O2TestDPRs",
                column: "DPRFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PM5TestDPRs_DPRFieldId",
                table: "PM5TestDPRs",
                column: "DPRFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PMTestDPRs_DPRFieldId",
                table: "PMTestDPRs",
                column: "DPRFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SO2TestDPRs_DPRFieldId",
                table: "SO2TestDPRs",
                column: "DPRFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TempTestDPRs_DPRFieldId",
                table: "TempTestDPRs",
                column: "DPRFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VOCTestDPRs_DPRFieldId",
                table: "VOCTestDPRs",
                column: "DPRFieldId",
                unique: true);
        }
    }
}
