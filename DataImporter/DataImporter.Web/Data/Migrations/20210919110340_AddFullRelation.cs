using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataImporter.Web.Data.Migrations
{
    public partial class AddFullRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExcelData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExcelData_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExcelFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExcelFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExcelFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExcelFile_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExcelFieldData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExcelDataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelFieldData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExcelFieldData_ExcelData_ExcelDataId",
                        column: x => x.ExcelDataId,
                        principalTable: "ExcelData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExcelData_GroupId",
                table: "ExcelData",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ExcelFieldData_ExcelDataId",
                table: "ExcelFieldData",
                column: "ExcelDataId");

            migrationBuilder.CreateIndex(
                name: "IX_ExcelFile_GroupId",
                table: "ExcelFile",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExcelFieldData");

            migrationBuilder.DropTable(
                name: "ExcelFile");

            migrationBuilder.DropTable(
                name: "ExcelData");

            migrationBuilder.DropTable(
                name: "Group");
        }
    }
}
