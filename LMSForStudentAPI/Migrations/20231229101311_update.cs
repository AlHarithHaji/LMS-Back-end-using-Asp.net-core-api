using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSForStudentAPI.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "LoginTb",
                type: "NUMBER(10)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "LoginTb");
        }
    }
}
