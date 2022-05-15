using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PAMS.Persistence.Migrations
{
    public partial class AddedImageModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnalystFullName",
                table: "NESREAFields",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ImageModelId",
                table: "NESREAFields",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnalystFullName",
                table: "FMENVFields",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ImageModelId",
                table: "FMENVFields",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnalystFullName",
                table: "DPRFields",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ImageModelId",
                table: "DPRFields",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ImageModelId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ImageModels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoBase64 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageModels", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NESREAFields_ImageModelId",
                table: "NESREAFields",
                column: "ImageModelId");

            migrationBuilder.CreateIndex(
                name: "IX_FMENVFields_ImageModelId",
                table: "FMENVFields",
                column: "ImageModelId");

            migrationBuilder.CreateIndex(
                name: "IX_DPRFields_ImageModelId",
                table: "DPRFields",
                column: "ImageModelId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ImageModelId",
                table: "AspNetUsers",
                column: "ImageModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ImageModels_ImageModelId",
                table: "AspNetUsers",
                column: "ImageModelId",
                principalTable: "ImageModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DPRFields_ImageModels_ImageModelId",
                table: "DPRFields",
                column: "ImageModelId",
                principalTable: "ImageModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FMENVFields_ImageModels_ImageModelId",
                table: "FMENVFields",
                column: "ImageModelId",
                principalTable: "ImageModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NESREAFields_ImageModels_ImageModelId",
                table: "NESREAFields",
                column: "ImageModelId",
                principalTable: "ImageModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ImageModels_ImageModelId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_DPRFields_ImageModels_ImageModelId",
                table: "DPRFields");

            migrationBuilder.DropForeignKey(
                name: "FK_FMENVFields_ImageModels_ImageModelId",
                table: "FMENVFields");

            migrationBuilder.DropForeignKey(
                name: "FK_NESREAFields_ImageModels_ImageModelId",
                table: "NESREAFields");

            migrationBuilder.DropTable(
                name: "ImageModels");

            migrationBuilder.DropIndex(
                name: "IX_NESREAFields_ImageModelId",
                table: "NESREAFields");

            migrationBuilder.DropIndex(
                name: "IX_FMENVFields_ImageModelId",
                table: "FMENVFields");

            migrationBuilder.DropIndex(
                name: "IX_DPRFields_ImageModelId",
                table: "DPRFields");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ImageModelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AnalystFullName",
                table: "NESREAFields");

            migrationBuilder.DropColumn(
                name: "ImageModelId",
                table: "NESREAFields");

            migrationBuilder.DropColumn(
                name: "AnalystFullName",
                table: "FMENVFields");

            migrationBuilder.DropColumn(
                name: "ImageModelId",
                table: "FMENVFields");

            migrationBuilder.DropColumn(
                name: "AnalystFullName",
                table: "DPRFields");

            migrationBuilder.DropColumn(
                name: "ImageModelId",
                table: "DPRFields");

            migrationBuilder.DropColumn(
                name: "ImageModelId",
                table: "AspNetUsers");
        }
    }
}
