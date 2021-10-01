using Microsoft.EntityFrameworkCore.Migrations;

namespace DataImporter.Web.Migrations.ImportingDb
{
    public partial class addGroupId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "ExportFileHistory");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "ExportFileHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ExportFileHistory_GroupId",
                table: "ExportFileHistory",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExportFileHistory_Group_GroupId",
                table: "ExportFileHistory",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExportFileHistory_Group_GroupId",
                table: "ExportFileHistory");

            migrationBuilder.DropIndex(
                name: "IX_ExportFileHistory_GroupId",
                table: "ExportFileHistory");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "ExportFileHistory");

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "ExportFileHistory",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
