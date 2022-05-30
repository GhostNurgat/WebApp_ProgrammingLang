using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp_ProgrammingLang.Migrations
{
    public partial class AddTaskWorkMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Filename",
                table: "TaskWorks",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "TaskWorks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TaskWorkMessage",
                columns: table => new
                {
                    TaskWorkID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateMessage = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Filename = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TaskWorkID1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskWorkMessage", x => x.TaskWorkID);
                    table.ForeignKey(
                        name: "FK_TaskWorkMessage_TaskWorks_TaskWorkID1",
                        column: x => x.TaskWorkID1,
                        principalTable: "TaskWorks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskWorkMessage_TaskWorkID1",
                table: "TaskWorkMessage",
                column: "TaskWorkID1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskWorkMessage");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "TaskWorks");

            migrationBuilder.AlterColumn<string>(
                name: "Filename",
                table: "TaskWorks",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
