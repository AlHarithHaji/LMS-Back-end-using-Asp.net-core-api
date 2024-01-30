using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSForStudentAPI.Migrations
{
    public partial class AddAllocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllocationTeacherTb",
                columns: table => new
                {
                    AllocationTeacherId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ClassRoomId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TeacherId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CourseId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocationTeacherTb", x => x.AllocationTeacherId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllocationTeacherTb");
        }
    }
}
