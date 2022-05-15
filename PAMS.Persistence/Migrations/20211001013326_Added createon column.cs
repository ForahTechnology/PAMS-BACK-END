using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PAMS.Persistence.Migrations
{
    public partial class Addedcreateoncolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MicroBiologicalAnalyses_Reports_ReportID",
                table: "MicroBiologicalAnalyses");

            migrationBuilder.DropForeignKey(
                name: "FK_PhysicoChemicalAnalyses_Reports_ReportID",
                table: "PhysicoChemicalAnalyses");

            migrationBuilder.DropForeignKey(
                name: "FK_Samples_Samplings_SamplingID",
                table: "Samples");

            migrationBuilder.DropTable(
                name: "PhysicoChemicalAnalysisTemplates");

            migrationBuilder.DropIndex(
                name: "IX_Samples_SamplingID",
                table: "Samples");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReportID",
                table: "PhysicoChemicalAnalyses",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "SamplingID",
                table: "PhysicoChemicalAnalyses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateOn",
                table: "NESREAs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "NESREAParameters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Test_Method",
                table: "NESREAParameters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UC",
                table: "NESREAParameters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ReportID",
                table: "MicroBiologicalAnalyses",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "SamplingID",
                table: "MicroBiologicalAnalyses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateOn",
                table: "FMVEnvs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "FMEnvParameterTemplates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Test_Method",
                table: "FMEnvParameterTemplates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UC",
                table: "FMEnvParameterTemplates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateOn",
                table: "DPRs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "DPRParameters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Test_Method",
                table: "DPRParameters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UC",
                table: "DPRParameters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhysicoChemicalAnalyses_SamplingID",
                table: "PhysicoChemicalAnalyses",
                column: "SamplingID");

            migrationBuilder.CreateIndex(
                name: "IX_MicroBiologicalAnalyses_SamplingID",
                table: "MicroBiologicalAnalyses",
                column: "SamplingID");

            migrationBuilder.AddForeignKey(
                name: "FK_MicroBiologicalAnalyses_Reports_ReportID",
                table: "MicroBiologicalAnalyses",
                column: "ReportID",
                principalTable: "Reports",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MicroBiologicalAnalyses_Samplings_SamplingID",
                table: "MicroBiologicalAnalyses",
                column: "SamplingID",
                principalTable: "Samplings",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhysicoChemicalAnalyses_Reports_ReportID",
                table: "PhysicoChemicalAnalyses",
                column: "ReportID",
                principalTable: "Reports",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PhysicoChemicalAnalyses_Samplings_SamplingID",
                table: "PhysicoChemicalAnalyses",
                column: "SamplingID",
                principalTable: "Samplings",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MicroBiologicalAnalyses_Reports_ReportID",
                table: "MicroBiologicalAnalyses");

            migrationBuilder.DropForeignKey(
                name: "FK_MicroBiologicalAnalyses_Samplings_SamplingID",
                table: "MicroBiologicalAnalyses");

            migrationBuilder.DropForeignKey(
                name: "FK_PhysicoChemicalAnalyses_Reports_ReportID",
                table: "PhysicoChemicalAnalyses");

            migrationBuilder.DropForeignKey(
                name: "FK_PhysicoChemicalAnalyses_Samplings_SamplingID",
                table: "PhysicoChemicalAnalyses");

            migrationBuilder.DropIndex(
                name: "IX_PhysicoChemicalAnalyses_SamplingID",
                table: "PhysicoChemicalAnalyses");

            migrationBuilder.DropIndex(
                name: "IX_MicroBiologicalAnalyses_SamplingID",
                table: "MicroBiologicalAnalyses");

            migrationBuilder.DropColumn(
                name: "SamplingID",
                table: "PhysicoChemicalAnalyses");

            migrationBuilder.DropColumn(
                name: "CreateOn",
                table: "NESREAs");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "NESREAParameters");

            migrationBuilder.DropColumn(
                name: "Test_Method",
                table: "NESREAParameters");

            migrationBuilder.DropColumn(
                name: "UC",
                table: "NESREAParameters");

            migrationBuilder.DropColumn(
                name: "SamplingID",
                table: "MicroBiologicalAnalyses");

            migrationBuilder.DropColumn(
                name: "CreateOn",
                table: "FMVEnvs");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "FMEnvParameterTemplates");

            migrationBuilder.DropColumn(
                name: "Test_Method",
                table: "FMEnvParameterTemplates");

            migrationBuilder.DropColumn(
                name: "UC",
                table: "FMEnvParameterTemplates");

            migrationBuilder.DropColumn(
                name: "CreateOn",
                table: "DPRs");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "DPRParameters");

            migrationBuilder.DropColumn(
                name: "Test_Method",
                table: "DPRParameters");

            migrationBuilder.DropColumn(
                name: "UC",
                table: "DPRParameters");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReportID",
                table: "PhysicoChemicalAnalyses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ReportID",
                table: "MicroBiologicalAnalyses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PhysicoChemicalAnalysisTemplates",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Limit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Test_Method = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Test_Performed_And_Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    UC = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicoChemicalAnalysisTemplates", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Samples_SamplingID",
                table: "Samples",
                column: "SamplingID");

            migrationBuilder.AddForeignKey(
                name: "FK_MicroBiologicalAnalyses_Reports_ReportID",
                table: "MicroBiologicalAnalyses",
                column: "ReportID",
                principalTable: "Reports",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhysicoChemicalAnalyses_Reports_ReportID",
                table: "PhysicoChemicalAnalyses",
                column: "ReportID",
                principalTable: "Reports",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Samples_Samplings_SamplingID",
                table: "Samples",
                column: "SamplingID",
                principalTable: "Samplings",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
