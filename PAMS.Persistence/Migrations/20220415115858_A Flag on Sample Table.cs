using Microsoft.EntityFrameworkCore.Migrations;

namespace PAMS.Persistence.Migrations
{
    public partial class AFlagonSampleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReportCreated",
                table: "Samplings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReportCreated",
                table: "Samplings");
        }
    }
}
