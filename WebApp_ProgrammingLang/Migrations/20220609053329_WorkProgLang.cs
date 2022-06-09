using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp_ProgrammingLang.Migrations
{
    public partial class WorkProgLang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LanguageID",
                table: "Works",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProgrammingLanguageID",
                table: "Works",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Works_ProgrammingLanguageID",
                table: "Works",
                column: "ProgrammingLanguageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Works_ProgrammingLanguages_ProgrammingLanguageID",
                table: "Works",
                column: "ProgrammingLanguageID",
                principalTable: "ProgrammingLanguages",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_ProgrammingLanguages_ProgrammingLanguageID",
                table: "Works");

            migrationBuilder.DropIndex(
                name: "IX_Works_ProgrammingLanguageID",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "LanguageID",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "ProgrammingLanguageID",
                table: "Works");
        }
    }
}
