using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PAMS.Persistence.Migrations
{
    public partial class AddAirqualitysampleandparameters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AirQualitySamples",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirQualitySamples", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AirQualitySampleParameters",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AirQualitySampleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Limit = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirQualitySampleParameters", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AirQualitySampleParameters_AirQualitySamples_AirQualitySampleId",
                        column: x => x.AirQualitySampleId,
                        principalTable: "AirQualitySamples",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirQualitySampleParameters_AirQualitySampleId",
                table: "AirQualitySampleParameters",
                column: "AirQualitySampleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirQualitySampleParameters");

            migrationBuilder.DropTable(
                name: "AirQualitySamples");
        }
    }
}
