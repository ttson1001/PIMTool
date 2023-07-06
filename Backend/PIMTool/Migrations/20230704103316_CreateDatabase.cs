using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PIMTool.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EMPLOYEE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VISA = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    FIRST_NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LAST_NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BIRTH_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VERSION = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPLOYEE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GROUP",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GROUP_LEADER_ID = table.Column<int>(type: "int", nullable: true),
                    VERSION = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GROUP", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GROUP_EMPLOYEE_GROUP_LEADER_ID",
                        column: x => x.GROUP_LEADER_ID,
                        principalTable: "EMPLOYEE",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PROJECT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PROJECT_NUMBER = table.Column<int>(type: "int", nullable: false),
                    CUSTOMER = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    STATUS = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    START_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    END_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    VERSION = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROJECT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PROJECT_GROUP_GroupId",
                        column: x => x.GroupId,
                        principalTable: "GROUP",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PROJECT_EMPLOYEE",
                columns: table => new
                {
                    PROJECT_ID = table.Column<int>(type: "int", nullable: false),
                    EMPLOYEE_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROJECT_EMPLOYEE", x => new { x.PROJECT_ID, x.EMPLOYEE_ID });
                    table.ForeignKey(
                        name: "FK_PROJECT_EMPLOYEE_EMPLOYEE_EMPLOYEE_ID",
                        column: x => x.EMPLOYEE_ID,
                        principalTable: "EMPLOYEE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PROJECT_EMPLOYEE_PROJECT_PROJECT_ID",
                        column: x => x.PROJECT_ID,
                        principalTable: "PROJECT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GROUP_GROUP_LEADER_ID",
                table: "GROUP",
                column: "GROUP_LEADER_ID",
                unique: true,
                filter: "[GROUP_LEADER_ID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PROJECT_GroupId",
                table: "PROJECT",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PROJECT_EMPLOYEE_EMPLOYEE_ID",
                table: "PROJECT_EMPLOYEE",
                column: "EMPLOYEE_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PROJECT_EMPLOYEE");

            migrationBuilder.DropTable(
                name: "PROJECT");

            migrationBuilder.DropTable(
                name: "GROUP");

            migrationBuilder.DropTable(
                name: "EMPLOYEE");
        }
    }
}
