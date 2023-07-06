using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PIMTool.Migrations
{
    public partial class IgnoreID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "PROJECT_EMPLOYEE",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PROJECT_EMPLOYEE",
                newName: "ID");
        }
    }
}
