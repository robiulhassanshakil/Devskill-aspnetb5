using Microsoft.EntityFrameworkCore.Migrations;

namespace DataImporter.Web.Migrations.ImportingDb
{
    public partial class addExcelHistoryGroupName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "ExportFileHistory",
                newName: "GroupName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GroupName",
                table: "ExportFileHistory",
                newName: "FileName");
        }
    }
}
