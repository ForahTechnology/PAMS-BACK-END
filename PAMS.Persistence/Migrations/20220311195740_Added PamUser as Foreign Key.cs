using Microsoft.EntityFrameworkCore.Migrations;

namespace PAMS.Persistence.Migrations
{
    public partial class AddedPamUserasForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnalystFullName",
                table: "NESREAFields");

            migrationBuilder.DropColumn(
                name: "AnalystFullName",
                table: "FMENVFields");

            migrationBuilder.DropColumn(
                name: "AnalystFullName",
                table: "DPRFields");

            migrationBuilder.AddColumn<string>(
                name: "PamsUserId",
                table: "NESREAFields",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PamsUserId",
                table: "FMENVFields",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PamsUserId",
                table: "DPRFields",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NESREAFields_PamsUserId",
                table: "NESREAFields",
                column: "PamsUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FMENVFields_PamsUserId",
                table: "FMENVFields",
                column: "PamsUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DPRFields_PamsUserId",
                table: "DPRFields",
                column: "PamsUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DPRFields_AspNetUsers_PamsUserId",
                table: "DPRFields",
                column: "PamsUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FMENVFields_AspNetUsers_PamsUserId",
                table: "FMENVFields",
                column: "PamsUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NESREAFields_AspNetUsers_PamsUserId",
                table: "NESREAFields",
                column: "PamsUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DPRFields_AspNetUsers_PamsUserId",
                table: "DPRFields");

            migrationBuilder.DropForeignKey(
                name: "FK_FMENVFields_AspNetUsers_PamsUserId",
                table: "FMENVFields");

            migrationBuilder.DropForeignKey(
                name: "FK_NESREAFields_AspNetUsers_PamsUserId",
                table: "NESREAFields");

            migrationBuilder.DropIndex(
                name: "IX_NESREAFields_PamsUserId",
                table: "NESREAFields");

            migrationBuilder.DropIndex(
                name: "IX_FMENVFields_PamsUserId",
                table: "FMENVFields");

            migrationBuilder.DropIndex(
                name: "IX_DPRFields_PamsUserId",
                table: "DPRFields");

            migrationBuilder.DropColumn(
                name: "PamsUserId",
                table: "NESREAFields");

            migrationBuilder.DropColumn(
                name: "PamsUserId",
                table: "FMENVFields");

            migrationBuilder.DropColumn(
                name: "PamsUserId",
                table: "DPRFields");

            migrationBuilder.AddColumn<string>(
                name: "AnalystFullName",
                table: "NESREAFields",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnalystFullName",
                table: "FMENVFields",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnalystFullName",
                table: "DPRFields",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
