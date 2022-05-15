using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PAMS.Persistence.Migrations
{
    public partial class RemodifiedSample : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DPRFields_SamplePointLocations_SamplePointLocationId",
                table: "DPRFields");

            migrationBuilder.DropForeignKey(
                name: "FK_FMENVFields_SamplePointLocations_SamplePointLocationId",
                table: "FMENVFields");

            migrationBuilder.DropForeignKey(
                name: "FK_NESREAFields_SamplePointLocations_SamplePointLocationId",
                table: "NESREAFields");

            migrationBuilder.DropForeignKey(
                name: "FK_SamplePointLocations_Clients_ClientID",
                table: "SamplePointLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_Samplings_Clients_ClientID",
                table: "Samplings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SamplePointLocations",
                table: "SamplePointLocations");

            migrationBuilder.RenameTable(
                name: "SamplePointLocations",
                newName: "SamplePointLocation");

            migrationBuilder.RenameIndex(
                name: "IX_SamplePointLocations_ClientID",
                table: "SamplePointLocation",
                newName: "IX_SamplePointLocation_ClientID");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientID",
                table: "Samplings",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<long>(
                name: "SamplePointLocationId",
                table: "Samplings",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SamplePointLocation",
                table: "SamplePointLocation",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Samplings_SamplePointLocationId",
                table: "Samplings",
                column: "SamplePointLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_DPRFields_SamplePointLocation_SamplePointLocationId",
                table: "DPRFields",
                column: "SamplePointLocationId",
                principalTable: "SamplePointLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FMENVFields_SamplePointLocation_SamplePointLocationId",
                table: "FMENVFields",
                column: "SamplePointLocationId",
                principalTable: "SamplePointLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NESREAFields_SamplePointLocation_SamplePointLocationId",
                table: "NESREAFields",
                column: "SamplePointLocationId",
                principalTable: "SamplePointLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SamplePointLocation_Clients_ClientID",
                table: "SamplePointLocation",
                column: "ClientID",
                principalTable: "Clients",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Samplings_Clients_ClientID",
                table: "Samplings",
                column: "ClientID",
                principalTable: "Clients",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Samplings_SamplePointLocation_SamplePointLocationId",
                table: "Samplings",
                column: "SamplePointLocationId",
                principalTable: "SamplePointLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DPRFields_SamplePointLocation_SamplePointLocationId",
                table: "DPRFields");

            migrationBuilder.DropForeignKey(
                name: "FK_FMENVFields_SamplePointLocation_SamplePointLocationId",
                table: "FMENVFields");

            migrationBuilder.DropForeignKey(
                name: "FK_NESREAFields_SamplePointLocation_SamplePointLocationId",
                table: "NESREAFields");

            migrationBuilder.DropForeignKey(
                name: "FK_SamplePointLocation_Clients_ClientID",
                table: "SamplePointLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_Samplings_Clients_ClientID",
                table: "Samplings");

            migrationBuilder.DropForeignKey(
                name: "FK_Samplings_SamplePointLocation_SamplePointLocationId",
                table: "Samplings");

            migrationBuilder.DropIndex(
                name: "IX_Samplings_SamplePointLocationId",
                table: "Samplings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SamplePointLocation",
                table: "SamplePointLocation");

            migrationBuilder.DropColumn(
                name: "SamplePointLocationId",
                table: "Samplings");

            migrationBuilder.RenameTable(
                name: "SamplePointLocation",
                newName: "SamplePointLocations");

            migrationBuilder.RenameIndex(
                name: "IX_SamplePointLocation_ClientID",
                table: "SamplePointLocations",
                newName: "IX_SamplePointLocations_ClientID");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientID",
                table: "Samplings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SamplePointLocations",
                table: "SamplePointLocations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DPRFields_SamplePointLocations_SamplePointLocationId",
                table: "DPRFields",
                column: "SamplePointLocationId",
                principalTable: "SamplePointLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FMENVFields_SamplePointLocations_SamplePointLocationId",
                table: "FMENVFields",
                column: "SamplePointLocationId",
                principalTable: "SamplePointLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NESREAFields_SamplePointLocations_SamplePointLocationId",
                table: "NESREAFields",
                column: "SamplePointLocationId",
                principalTable: "SamplePointLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SamplePointLocations_Clients_ClientID",
                table: "SamplePointLocations",
                column: "ClientID",
                principalTable: "Clients",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Samplings_Clients_ClientID",
                table: "Samplings",
                column: "ClientID",
                principalTable: "Clients",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
