using Microsoft.EntityFrameworkCore.Migrations;

namespace PAMS.Persistence.Migrations
{
    public partial class UpdatedFieldScientistEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VOCTests_NESREAFieldId",
                table: "VOCTests");

            migrationBuilder.DropIndex(
                name: "IX_VOCTestFMs_FMENVFieldId",
                table: "VOCTestFMs");

            migrationBuilder.DropIndex(
                name: "IX_VOCTestDPRs_DPRFieldId",
                table: "VOCTestDPRs");

            migrationBuilder.DropIndex(
                name: "IX_TempTests_NESREAFieldId",
                table: "TempTests");

            migrationBuilder.DropIndex(
                name: "IX_TempTestFMs_FMENVFieldId",
                table: "TempTestFMs");

            migrationBuilder.DropIndex(
                name: "IX_TempTestDPRs_DPRFieldId",
                table: "TempTestDPRs");

            migrationBuilder.DropIndex(
                name: "IX_SO2Tests_NESREAFieldId",
                table: "SO2Tests");

            migrationBuilder.DropIndex(
                name: "IX_SO2TestFMs_FMENVFieldId",
                table: "SO2TestFMs");

            migrationBuilder.DropIndex(
                name: "IX_SO2TestDPRs_DPRFieldId",
                table: "SO2TestDPRs");

            migrationBuilder.DropIndex(
                name: "IX_PMTests_NESREAFieldId",
                table: "PMTests");

            migrationBuilder.DropIndex(
                name: "IX_PMTestFMs_FMENVFieldId",
                table: "PMTestFMs");

            migrationBuilder.DropIndex(
                name: "IX_PMTestDPRs_DPRFieldId",
                table: "PMTestDPRs");

            migrationBuilder.DropIndex(
                name: "IX_PM5Tests_NESREAFieldId",
                table: "PM5Tests");

            migrationBuilder.DropIndex(
                name: "IX_PM5TestFMs_FMENVFieldId",
                table: "PM5TestFMs");

            migrationBuilder.DropIndex(
                name: "IX_PM5TestDPRs_DPRFieldId",
                table: "PM5TestDPRs");

            migrationBuilder.DropIndex(
                name: "IX_O2Tests_NESREAFieldId",
                table: "O2Tests");

            migrationBuilder.DropIndex(
                name: "IX_O2TestFMs_FMENVFieldId",
                table: "O2TestFMs");

            migrationBuilder.DropIndex(
                name: "IX_O2TestDPRs_DPRFieldId",
                table: "O2TestDPRs");

            migrationBuilder.DropIndex(
                name: "IX_NoiseTests_NESREAFieldId",
                table: "NoiseTests");

            migrationBuilder.DropIndex(
                name: "IX_NoiseTestFMs_FMENVFieldId",
                table: "NoiseTestFMs");

            migrationBuilder.DropIndex(
                name: "IX_NoiseTestDPRs_DPRFieldId",
                table: "NoiseTestDPRs");

            migrationBuilder.DropIndex(
                name: "IX_NO2Tests_NESREAFieldId",
                table: "NO2Tests");

            migrationBuilder.DropIndex(
                name: "IX_NO2TestFMs_FMENVFieldId",
                table: "NO2TestFMs");

            migrationBuilder.DropIndex(
                name: "IX_NO2TestDPRs_DPRFieldId",
                table: "NO2TestDPRs");

            migrationBuilder.DropIndex(
                name: "IX_HMTests_NESREAFieldId",
                table: "HMTests");

            migrationBuilder.DropIndex(
                name: "IX_HMTestFMs_FMENVFieldId",
                table: "HMTestFMs");

            migrationBuilder.DropIndex(
                name: "IX_HMTestDPRs_DPRFieldId",
                table: "HMTestDPRs");

            migrationBuilder.DropIndex(
                name: "IX_H2STests_NESREAFieldId",
                table: "H2STests");

            migrationBuilder.DropIndex(
                name: "IX_H2STestFMs_FMENVFieldId",
                table: "H2STestFMs");

            migrationBuilder.DropIndex(
                name: "IX_H2STestDPRs_DPRFieldId",
                table: "H2STestDPRs");

            migrationBuilder.DropIndex(
                name: "IX_FieldLocations_DPRFieldId",
                table: "FieldLocations");

            migrationBuilder.DropIndex(
                name: "IX_FieldLocations_FMENVFieldId",
                table: "FieldLocations");

            migrationBuilder.DropIndex(
                name: "IX_FieldLocations_NESREAFieldId",
                table: "FieldLocations");

            migrationBuilder.DropIndex(
                name: "IX_COTests_NESREAFieldId",
                table: "COTests");

            migrationBuilder.DropIndex(
                name: "IX_COTestFMs_FMENVFieldId",
                table: "COTestFMs");

            migrationBuilder.DropIndex(
                name: "IX_COTestDPRs_DPRFieldId",
                table: "COTestDPRs");

            migrationBuilder.DropIndex(
                name: "IX_COMBTests_NESREAFieldId",
                table: "COMBTests");

            migrationBuilder.DropIndex(
                name: "IX_COMBTestFMs_FMENVFieldId",
                table: "COMBTestFMs");

            migrationBuilder.DropIndex(
                name: "IX_COMBTestDPRs_DPRFieldId",
                table: "COMBTestDPRs");

            migrationBuilder.DropIndex(
                name: "IX_CO2Tests_NESREAFieldId",
                table: "CO2Tests");

            migrationBuilder.DropIndex(
                name: "IX_CO2TestFMs_FMENVFieldId",
                table: "CO2TestFMs");

            migrationBuilder.DropIndex(
                name: "IX_CO2TestDPRs_DPRFieldId",
                table: "CO2TestDPRs");

            migrationBuilder.CreateIndex(
                name: "IX_VOCTests_NESREAFieldId",
                table: "VOCTests",
                column: "NESREAFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VOCTestFMs_FMENVFieldId",
                table: "VOCTestFMs",
                column: "FMENVFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VOCTestDPRs_DPRFieldId",
                table: "VOCTestDPRs",
                column: "DPRFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TempTests_NESREAFieldId",
                table: "TempTests",
                column: "NESREAFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TempTestFMs_FMENVFieldId",
                table: "TempTestFMs",
                column: "FMENVFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TempTestDPRs_DPRFieldId",
                table: "TempTestDPRs",
                column: "DPRFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SO2Tests_NESREAFieldId",
                table: "SO2Tests",
                column: "NESREAFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SO2TestFMs_FMENVFieldId",
                table: "SO2TestFMs",
                column: "FMENVFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SO2TestDPRs_DPRFieldId",
                table: "SO2TestDPRs",
                column: "DPRFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PMTests_NESREAFieldId",
                table: "PMTests",
                column: "NESREAFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PMTestFMs_FMENVFieldId",
                table: "PMTestFMs",
                column: "FMENVFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PMTestDPRs_DPRFieldId",
                table: "PMTestDPRs",
                column: "DPRFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PM5Tests_NESREAFieldId",
                table: "PM5Tests",
                column: "NESREAFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PM5TestFMs_FMENVFieldId",
                table: "PM5TestFMs",
                column: "FMENVFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PM5TestDPRs_DPRFieldId",
                table: "PM5TestDPRs",
                column: "DPRFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_O2Tests_NESREAFieldId",
                table: "O2Tests",
                column: "NESREAFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_O2TestFMs_FMENVFieldId",
                table: "O2TestFMs",
                column: "FMENVFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_O2TestDPRs_DPRFieldId",
                table: "O2TestDPRs",
                column: "DPRFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NoiseTests_NESREAFieldId",
                table: "NoiseTests",
                column: "NESREAFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NoiseTestFMs_FMENVFieldId",
                table: "NoiseTestFMs",
                column: "FMENVFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NoiseTestDPRs_DPRFieldId",
                table: "NoiseTestDPRs",
                column: "DPRFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NO2Tests_NESREAFieldId",
                table: "NO2Tests",
                column: "NESREAFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NO2TestFMs_FMENVFieldId",
                table: "NO2TestFMs",
                column: "FMENVFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NO2TestDPRs_DPRFieldId",
                table: "NO2TestDPRs",
                column: "DPRFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HMTests_NESREAFieldId",
                table: "HMTests",
                column: "NESREAFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HMTestFMs_FMENVFieldId",
                table: "HMTestFMs",
                column: "FMENVFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HMTestDPRs_DPRFieldId",
                table: "HMTestDPRs",
                column: "DPRFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_H2STests_NESREAFieldId",
                table: "H2STests",
                column: "NESREAFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_H2STestFMs_FMENVFieldId",
                table: "H2STestFMs",
                column: "FMENVFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_H2STestDPRs_DPRFieldId",
                table: "H2STestDPRs",
                column: "DPRFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FieldLocations_DPRFieldId",
                table: "FieldLocations",
                column: "DPRFieldId",
                unique: true,
                filter: "[DPRFieldId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FieldLocations_FMENVFieldId",
                table: "FieldLocations",
                column: "FMENVFieldId",
                unique: true,
                filter: "[FMENVFieldId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FieldLocations_NESREAFieldId",
                table: "FieldLocations",
                column: "NESREAFieldId",
                unique: true,
                filter: "[NESREAFieldId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_COTests_NESREAFieldId",
                table: "COTests",
                column: "NESREAFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_COTestFMs_FMENVFieldId",
                table: "COTestFMs",
                column: "FMENVFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_COTestDPRs_DPRFieldId",
                table: "COTestDPRs",
                column: "DPRFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_COMBTests_NESREAFieldId",
                table: "COMBTests",
                column: "NESREAFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_COMBTestFMs_FMENVFieldId",
                table: "COMBTestFMs",
                column: "FMENVFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_COMBTestDPRs_DPRFieldId",
                table: "COMBTestDPRs",
                column: "DPRFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CO2Tests_NESREAFieldId",
                table: "CO2Tests",
                column: "NESREAFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CO2TestFMs_FMENVFieldId",
                table: "CO2TestFMs",
                column: "FMENVFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CO2TestDPRs_DPRFieldId",
                table: "CO2TestDPRs",
                column: "DPRFieldId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VOCTests_NESREAFieldId",
                table: "VOCTests");

            migrationBuilder.DropIndex(
                name: "IX_VOCTestFMs_FMENVFieldId",
                table: "VOCTestFMs");

            migrationBuilder.DropIndex(
                name: "IX_VOCTestDPRs_DPRFieldId",
                table: "VOCTestDPRs");

            migrationBuilder.DropIndex(
                name: "IX_TempTests_NESREAFieldId",
                table: "TempTests");

            migrationBuilder.DropIndex(
                name: "IX_TempTestFMs_FMENVFieldId",
                table: "TempTestFMs");

            migrationBuilder.DropIndex(
                name: "IX_TempTestDPRs_DPRFieldId",
                table: "TempTestDPRs");

            migrationBuilder.DropIndex(
                name: "IX_SO2Tests_NESREAFieldId",
                table: "SO2Tests");

            migrationBuilder.DropIndex(
                name: "IX_SO2TestFMs_FMENVFieldId",
                table: "SO2TestFMs");

            migrationBuilder.DropIndex(
                name: "IX_SO2TestDPRs_DPRFieldId",
                table: "SO2TestDPRs");

            migrationBuilder.DropIndex(
                name: "IX_PMTests_NESREAFieldId",
                table: "PMTests");

            migrationBuilder.DropIndex(
                name: "IX_PMTestFMs_FMENVFieldId",
                table: "PMTestFMs");

            migrationBuilder.DropIndex(
                name: "IX_PMTestDPRs_DPRFieldId",
                table: "PMTestDPRs");

            migrationBuilder.DropIndex(
                name: "IX_PM5Tests_NESREAFieldId",
                table: "PM5Tests");

            migrationBuilder.DropIndex(
                name: "IX_PM5TestFMs_FMENVFieldId",
                table: "PM5TestFMs");

            migrationBuilder.DropIndex(
                name: "IX_PM5TestDPRs_DPRFieldId",
                table: "PM5TestDPRs");

            migrationBuilder.DropIndex(
                name: "IX_O2Tests_NESREAFieldId",
                table: "O2Tests");

            migrationBuilder.DropIndex(
                name: "IX_O2TestFMs_FMENVFieldId",
                table: "O2TestFMs");

            migrationBuilder.DropIndex(
                name: "IX_O2TestDPRs_DPRFieldId",
                table: "O2TestDPRs");

            migrationBuilder.DropIndex(
                name: "IX_NoiseTests_NESREAFieldId",
                table: "NoiseTests");

            migrationBuilder.DropIndex(
                name: "IX_NoiseTestFMs_FMENVFieldId",
                table: "NoiseTestFMs");

            migrationBuilder.DropIndex(
                name: "IX_NoiseTestDPRs_DPRFieldId",
                table: "NoiseTestDPRs");

            migrationBuilder.DropIndex(
                name: "IX_NO2Tests_NESREAFieldId",
                table: "NO2Tests");

            migrationBuilder.DropIndex(
                name: "IX_NO2TestFMs_FMENVFieldId",
                table: "NO2TestFMs");

            migrationBuilder.DropIndex(
                name: "IX_NO2TestDPRs_DPRFieldId",
                table: "NO2TestDPRs");

            migrationBuilder.DropIndex(
                name: "IX_HMTests_NESREAFieldId",
                table: "HMTests");

            migrationBuilder.DropIndex(
                name: "IX_HMTestFMs_FMENVFieldId",
                table: "HMTestFMs");

            migrationBuilder.DropIndex(
                name: "IX_HMTestDPRs_DPRFieldId",
                table: "HMTestDPRs");

            migrationBuilder.DropIndex(
                name: "IX_H2STests_NESREAFieldId",
                table: "H2STests");

            migrationBuilder.DropIndex(
                name: "IX_H2STestFMs_FMENVFieldId",
                table: "H2STestFMs");

            migrationBuilder.DropIndex(
                name: "IX_H2STestDPRs_DPRFieldId",
                table: "H2STestDPRs");

            migrationBuilder.DropIndex(
                name: "IX_FieldLocations_DPRFieldId",
                table: "FieldLocations");

            migrationBuilder.DropIndex(
                name: "IX_FieldLocations_FMENVFieldId",
                table: "FieldLocations");

            migrationBuilder.DropIndex(
                name: "IX_FieldLocations_NESREAFieldId",
                table: "FieldLocations");

            migrationBuilder.DropIndex(
                name: "IX_COTests_NESREAFieldId",
                table: "COTests");

            migrationBuilder.DropIndex(
                name: "IX_COTestFMs_FMENVFieldId",
                table: "COTestFMs");

            migrationBuilder.DropIndex(
                name: "IX_COTestDPRs_DPRFieldId",
                table: "COTestDPRs");

            migrationBuilder.DropIndex(
                name: "IX_COMBTests_NESREAFieldId",
                table: "COMBTests");

            migrationBuilder.DropIndex(
                name: "IX_COMBTestFMs_FMENVFieldId",
                table: "COMBTestFMs");

            migrationBuilder.DropIndex(
                name: "IX_COMBTestDPRs_DPRFieldId",
                table: "COMBTestDPRs");

            migrationBuilder.DropIndex(
                name: "IX_CO2Tests_NESREAFieldId",
                table: "CO2Tests");

            migrationBuilder.DropIndex(
                name: "IX_CO2TestFMs_FMENVFieldId",
                table: "CO2TestFMs");

            migrationBuilder.DropIndex(
                name: "IX_CO2TestDPRs_DPRFieldId",
                table: "CO2TestDPRs");

            migrationBuilder.CreateIndex(
                name: "IX_VOCTests_NESREAFieldId",
                table: "VOCTests",
                column: "NESREAFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_VOCTestFMs_FMENVFieldId",
                table: "VOCTestFMs",
                column: "FMENVFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_VOCTestDPRs_DPRFieldId",
                table: "VOCTestDPRs",
                column: "DPRFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_TempTests_NESREAFieldId",
                table: "TempTests",
                column: "NESREAFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_TempTestFMs_FMENVFieldId",
                table: "TempTestFMs",
                column: "FMENVFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_TempTestDPRs_DPRFieldId",
                table: "TempTestDPRs",
                column: "DPRFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_SO2Tests_NESREAFieldId",
                table: "SO2Tests",
                column: "NESREAFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_SO2TestFMs_FMENVFieldId",
                table: "SO2TestFMs",
                column: "FMENVFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_SO2TestDPRs_DPRFieldId",
                table: "SO2TestDPRs",
                column: "DPRFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_PMTests_NESREAFieldId",
                table: "PMTests",
                column: "NESREAFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_PMTestFMs_FMENVFieldId",
                table: "PMTestFMs",
                column: "FMENVFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_PMTestDPRs_DPRFieldId",
                table: "PMTestDPRs",
                column: "DPRFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_PM5Tests_NESREAFieldId",
                table: "PM5Tests",
                column: "NESREAFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_PM5TestFMs_FMENVFieldId",
                table: "PM5TestFMs",
                column: "FMENVFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_PM5TestDPRs_DPRFieldId",
                table: "PM5TestDPRs",
                column: "DPRFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_O2Tests_NESREAFieldId",
                table: "O2Tests",
                column: "NESREAFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_O2TestFMs_FMENVFieldId",
                table: "O2TestFMs",
                column: "FMENVFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_O2TestDPRs_DPRFieldId",
                table: "O2TestDPRs",
                column: "DPRFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_NoiseTests_NESREAFieldId",
                table: "NoiseTests",
                column: "NESREAFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_NoiseTestFMs_FMENVFieldId",
                table: "NoiseTestFMs",
                column: "FMENVFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_NoiseTestDPRs_DPRFieldId",
                table: "NoiseTestDPRs",
                column: "DPRFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_NO2Tests_NESREAFieldId",
                table: "NO2Tests",
                column: "NESREAFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_NO2TestFMs_FMENVFieldId",
                table: "NO2TestFMs",
                column: "FMENVFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_NO2TestDPRs_DPRFieldId",
                table: "NO2TestDPRs",
                column: "DPRFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_HMTests_NESREAFieldId",
                table: "HMTests",
                column: "NESREAFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_HMTestFMs_FMENVFieldId",
                table: "HMTestFMs",
                column: "FMENVFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_HMTestDPRs_DPRFieldId",
                table: "HMTestDPRs",
                column: "DPRFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_H2STests_NESREAFieldId",
                table: "H2STests",
                column: "NESREAFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_H2STestFMs_FMENVFieldId",
                table: "H2STestFMs",
                column: "FMENVFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_H2STestDPRs_DPRFieldId",
                table: "H2STestDPRs",
                column: "DPRFieldId");

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
                name: "IX_COTests_NESREAFieldId",
                table: "COTests",
                column: "NESREAFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_COTestFMs_FMENVFieldId",
                table: "COTestFMs",
                column: "FMENVFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_COTestDPRs_DPRFieldId",
                table: "COTestDPRs",
                column: "DPRFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_COMBTests_NESREAFieldId",
                table: "COMBTests",
                column: "NESREAFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_COMBTestFMs_FMENVFieldId",
                table: "COMBTestFMs",
                column: "FMENVFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_COMBTestDPRs_DPRFieldId",
                table: "COMBTestDPRs",
                column: "DPRFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_CO2Tests_NESREAFieldId",
                table: "CO2Tests",
                column: "NESREAFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_CO2TestFMs_FMENVFieldId",
                table: "CO2TestFMs",
                column: "FMENVFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_CO2TestDPRs_DPRFieldId",
                table: "CO2TestDPRs",
                column: "DPRFieldId");
        }
    }
}
