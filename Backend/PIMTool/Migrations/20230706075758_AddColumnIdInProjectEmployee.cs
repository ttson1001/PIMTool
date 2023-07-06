﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PIMTool.Migrations
{
    public partial class AddColumnIdInProjectEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PROJECT_EMPLOYEE",
                table: "PROJECT_EMPLOYEE");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "PROJECT_EMPLOYEE",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PROJECT_EMPLOYEE",
                table: "PROJECT_EMPLOYEE",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_PROJECT_EMPLOYEE_PROJECT_ID",
                table: "PROJECT_EMPLOYEE",
                column: "PROJECT_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PROJECT_EMPLOYEE",
                table: "PROJECT_EMPLOYEE");

            migrationBuilder.DropIndex(
                name: "IX_PROJECT_EMPLOYEE_PROJECT_ID",
                table: "PROJECT_EMPLOYEE");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "PROJECT_EMPLOYEE");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PROJECT_EMPLOYEE",
                table: "PROJECT_EMPLOYEE",
                columns: new[] { "PROJECT_ID", "EMPLOYEE_ID" });
        }
    }
}
