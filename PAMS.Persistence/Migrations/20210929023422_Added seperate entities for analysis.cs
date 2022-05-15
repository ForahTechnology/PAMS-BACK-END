using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PAMS.Persistence.Migrations
{
    public partial class Addedseperateentitiesforanalysis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhysicoParameterTemplates");

            migrationBuilder.DropTable(
                name: "PhysicoTemplates");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "PhysicoChemicalAnalyses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "PhysicoId",
                table: "MicroBiologicalAnalysisTemplates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "DPRs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DPRs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NESREAs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NESREAs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DPRParameters",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DPRID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Parameter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Limit = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DPRParameters", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DPRParameters_DPRs_DPRID",
                        column: x => x.DPRID,
                        principalTable: "DPRs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NESREAParameters",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NESREAID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Parameter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Limit = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NESREAParameters", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NESREAParameters_NESREAs_NESREAID",
                        column: x => x.NESREAID,
                        principalTable: "NESREAs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DPRParameters_DPRID",
                table: "DPRParameters",
                column: "DPRID");

            migrationBuilder.CreateIndex(
                name: "IX_NESREAParameters_NESREAID",
                table: "NESREAParameters",
                column: "NESREAID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DPRParameters");

            migrationBuilder.DropTable(
                name: "NESREAParameters");

            migrationBuilder.DropTable(
                name: "DPRs");

            migrationBuilder.DropTable(
                name: "NESREAs");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "PhysicoChemicalAnalyses");

            migrationBuilder.DropColumn(
                name: "PhysicoId",
                table: "MicroBiologicalAnalysisTemplates");

            migrationBuilder.CreateTable(
                name: "PhysicoTemplates",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicoTemplates", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PhysicoParameterTemplates",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Limit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parameter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysicoTemplateID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicoParameterTemplates", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PhysicoParameterTemplates_PhysicoTemplates_PhysicoTemplateID",
                        column: x => x.PhysicoTemplateID,
                        principalTable: "PhysicoTemplates",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhysicoParameterTemplates_PhysicoTemplateID",
                table: "PhysicoParameterTemplates",
                column: "PhysicoTemplateID");
        }
    }
}
