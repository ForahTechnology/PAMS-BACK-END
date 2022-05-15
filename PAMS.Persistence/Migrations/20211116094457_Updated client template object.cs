using Microsoft.EntityFrameworkCore.Migrations;

namespace PAMS.Persistence.Migrations
{
    public partial class Updatedclienttemplateobject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SampleType",
                table: "Samplings");

            migrationBuilder.RenameColumn(
                name: "PicturePaths",
                table: "Samplings",
                newName: "Picture");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ClientSamples",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "ClientSamples");

            migrationBuilder.RenameColumn(
                name: "Picture",
                table: "Samplings",
                newName: "PicturePaths");

            migrationBuilder.AddColumn<int>(
                name: "SampleType",
                table: "Samplings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
