using Microsoft.EntityFrameworkCore.Migrations;

namespace DataImporter.Web.Migrations.ImportingDb
{
    public partial class addGroupWithContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Files",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Files_GroupId",
                table: "Files",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Groups_GroupId",
                table: "Files",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Groups_GroupId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_GroupId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Files");
        }
    }
}
