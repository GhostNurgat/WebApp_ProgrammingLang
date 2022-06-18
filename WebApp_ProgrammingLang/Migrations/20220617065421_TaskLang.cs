using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp_ProgrammingLang.Migrations
{
    public partial class TaskLang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_ProgrammingLanguages_ProgrammingLanguageID",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ProgrammingLanguageID",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ProgrammingLanguageID",
                table: "Tasks");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_LanguageID",
                table: "Tasks",
                column: "LanguageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_ProgrammingLanguages_LanguageID",
                table: "Tasks",
                column: "LanguageID",
                principalTable: "ProgrammingLanguages",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_ProgrammingLanguages_LanguageID",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_LanguageID",
                table: "Tasks");

            migrationBuilder.AddColumn<int>(
                name: "ProgrammingLanguageID",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProgrammingLanguageID",
                table: "Tasks",
                column: "ProgrammingLanguageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_ProgrammingLanguages_ProgrammingLanguageID",
                table: "Tasks",
                column: "ProgrammingLanguageID",
                principalTable: "ProgrammingLanguages",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
