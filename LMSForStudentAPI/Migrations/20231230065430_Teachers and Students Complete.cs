using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSForStudentAPI.Migrations
{
    public partial class TeachersandStudentsComplete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentAttandenceTb",
                columns: table => new
                {
                    StudentAttanenceId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DeparmentId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CourseId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    LoginId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    SessionId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Presents = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    TotalLectures = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAttandenceTb", x => x.StudentAttanenceId);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourseAllocationTb",
                columns: table => new
                {
                    StudentCourseAllocationId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CourseId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    LoginId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    SesstionId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IsCourseActive = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourseAllocationTb", x => x.StudentCourseAllocationId);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourseGradesTb",
                columns: table => new
                {
                    StudentCourseGradesId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    LoginId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CourseId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TeacherId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DeparmentId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TotalMarks = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    ObtainMarks = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Percentage = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourseGradesTb", x => x.StudentCourseGradesId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentAttandenceTb");

            migrationBuilder.DropTable(
                name: "StudentCourseAllocationTb");

            migrationBuilder.DropTable(
                name: "StudentCourseGradesTb");
        }
    }
}
