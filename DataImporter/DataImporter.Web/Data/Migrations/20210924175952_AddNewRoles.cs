using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataImporter.Web.Migrations.ApplicationDb
{
    public partial class AddNewRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("2a47dd41-8395-4eb6-8140-fe758f3eb54b"), "84d6c3b0-d4b0-41c3-bfcc-99950f6efbcf", "Importer", "IMPORTER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2a47dd41-8395-4eb6-8140-fe758f3eb54b"));
        }
    }
}
