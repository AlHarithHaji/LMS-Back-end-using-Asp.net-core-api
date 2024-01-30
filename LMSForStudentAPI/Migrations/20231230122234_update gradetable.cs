using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSForStudentAPI.Migrations
{
    public partial class updategradetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "StudentCourseGradesTb",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "StudentCourseGradesTb");
        }
    }
}
