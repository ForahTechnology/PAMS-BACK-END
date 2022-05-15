using Microsoft.EntityFrameworkCore.Migrations;

namespace PAMS.Persistence.Migrations
{
    public partial class ChangesInFieldAnalysisEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "VOCTests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "VOCTestFMs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "VOCTestDPRs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "TempTests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "TempTestFMs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "TempTestDPRs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "SO2Tests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "SO2TestFMs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "SO2TestDPRs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "PMTests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "PMTestFMs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "PMTestDPRs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "PM5Tests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "PM5TestFMs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "PM5TestDPRs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "O2Tests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "O2TestFMs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "O2TestDPRs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "NoiseTests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "NoiseTestFMs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "NoiseTestDPRs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "NO2Tests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "NO2TestFMs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "NO2TestDPRs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "HMTests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "HMTestFMs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "HMTestDPRs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "H2STests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "H2STestFMs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "H2STestDPRs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "COTests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "COTestFMs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "COTestDPRs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "COMBTests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "COMBTestFMs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "COMBTestDPRs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "CO2Tests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "CO2TestFMs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestLimit",
                table: "CO2TestDPRs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "VOCTests");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "VOCTestFMs");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "VOCTestDPRs");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "TempTests");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "TempTestFMs");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "TempTestDPRs");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "SO2Tests");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "SO2TestFMs");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "SO2TestDPRs");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "PMTests");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "PMTestFMs");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "PMTestDPRs");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "PM5Tests");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "PM5TestFMs");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "PM5TestDPRs");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "O2Tests");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "O2TestFMs");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "O2TestDPRs");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "NoiseTests");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "NoiseTestFMs");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "NoiseTestDPRs");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "NO2Tests");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "NO2TestFMs");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "NO2TestDPRs");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "HMTests");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "HMTestFMs");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "HMTestDPRs");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "H2STests");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "H2STestFMs");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "H2STestDPRs");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "COTests");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "COTestFMs");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "COTestDPRs");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "COMBTests");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "COMBTestFMs");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "COMBTestDPRs");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "CO2Tests");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "CO2TestFMs");

            migrationBuilder.DropColumn(
                name: "TestLimit",
                table: "CO2TestDPRs");
        }
    }
}
