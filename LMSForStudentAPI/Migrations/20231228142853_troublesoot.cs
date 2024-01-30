using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSForStudentAPI.Migrations
{
    public partial class troublesoot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassRoomTb",
                columns: table => new
                {
                    ClassRoomId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ClassRoom = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassRoomTb", x => x.ClassRoomId);
                });

            migrationBuilder.CreateTable(
                name: "CoursesTb",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DeparmentId = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    CourseName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesTb", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentTb",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DeparmentName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    IsPaiad = table.Column<bool>(type: "NUMBER(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentTb", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "LoginTb",
                columns: table => new
                {
                    LoginId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    FirstName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    LastName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Mobile = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Address = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    UserType = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    IsActive = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    DepartmentId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    SessionId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Password = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginTb", x => x.LoginId);
                });

            migrationBuilder.CreateTable(
                name: "SesstionTB",
                columns: table => new
                {
                    SesstionId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Session = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    IsActive = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SesstionTB", x => x.SesstionId);
                });

            migrationBuilder.CreateTable(
                name: "TeacherTb",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TeacherName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherTb", x => x.TeacherId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassRoomTb");

            migrationBuilder.DropTable(
                name: "CoursesTb");

            migrationBuilder.DropTable(
                name: "DepartmentTb");

            migrationBuilder.DropTable(
                name: "LoginTb");

            migrationBuilder.DropTable(
                name: "SesstionTB");

            migrationBuilder.DropTable(
                name: "TeacherTb");
        }
    }
}
