using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp_ProgrammingLang.Migrations
{
    public partial class InitialMentorBid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskWorkMessage_TaskWorks_TaskWorkID1",
                table: "TaskWorkMessage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskWorkMessage",
                table: "TaskWorkMessage");

            migrationBuilder.RenameTable(
                name: "TaskWorkMessage",
                newName: "TaskMessages");

            migrationBuilder.RenameIndex(
                name: "IX_TaskWorkMessage_TaskWorkID1",
                table: "TaskMessages",
                newName: "IX_TaskMessages_TaskWorkID1");

            migrationBuilder.AlterColumn<string>(
                name: "Platform",
                table: "ProgrammingLanguages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "Languages",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskMessages",
                table: "TaskMessages",
                column: "TaskWorkID");

            migrationBuilder.CreateTable(
                name: "MentorBids",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Languages = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MentorBids", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MentorBids_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MentorBids_UserID",
                table: "MentorBids",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskMessages_TaskWorks_TaskWorkID1",
                table: "TaskMessages",
                column: "TaskWorkID1",
                principalTable: "TaskWorks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskMessages_TaskWorks_TaskWorkID1",
                table: "TaskMessages");

            migrationBuilder.DropTable(
                name: "MentorBids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskMessages",
                table: "TaskMessages");

            migrationBuilder.DropColumn(
                name: "Languages",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "TaskMessages",
                newName: "TaskWorkMessage");

            migrationBuilder.RenameIndex(
                name: "IX_TaskMessages_TaskWorkID1",
                table: "TaskWorkMessage",
                newName: "IX_TaskWorkMessage_TaskWorkID1");

            migrationBuilder.AlterColumn<string>(
                name: "Platform",
                table: "ProgrammingLanguages",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskWorkMessage",
                table: "TaskWorkMessage",
                column: "TaskWorkID");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskWorkMessage_TaskWorks_TaskWorkID1",
                table: "TaskWorkMessage",
                column: "TaskWorkID1",
                principalTable: "TaskWorks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
