﻿// <auto-generated />
using System;
using Core.Entity.LMSDataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace LMSForStudentAPI.Migrations
{
    [DbContext(typeof(LMSDbDataContext))]
    [Migration("20231229101311_update")]
    partial class update
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Core.Domain.Models.AllocationTeacherTb", b =>
                {
                    b.Property<int>("AllocationTeacherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("AllocationTeacherId");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AllocationTeacherId"), 1L, 1);

                    b.Property<int>("ClassRoomId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("CourseId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("TIMESTAMP");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("TIMESTAMP");

                    b.Property<int>("TeacherId")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("AllocationTeacherId");

                    b.ToTable("AllocationTeacherTb");
                });

            modelBuilder.Entity("Core.Domain.Models.ClassRoomTb", b =>
                {
                    b.Property<int>("ClassRoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ClassRoomId");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClassRoomId"), 1L, 1);

                    b.Property<string>("ClassRoom")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("NUMBER(1)");

                    b.HasKey("ClassRoomId");

                    b.ToTable("ClassRoomTb");
                });

            modelBuilder.Entity("Core.domain.Models.CoursesTb", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("CourseId");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"), 1L, 1);

                    b.Property<string>("CourseName")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int?>("DeparmentId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("NUMBER(1)");

                    b.HasKey("CourseId");

                    b.ToTable("CoursesTb");
                });

            modelBuilder.Entity("Core.domain.Models.DepartmentTb", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentId"), 1L, 1);

                    b.Property<string>("DeparmentName")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<bool?>("IsPaiad")
                        .HasColumnType("NUMBER(1)");

                    b.HasKey("DepartmentId");

                    b.ToTable("DepartmentTb");
                });

            modelBuilder.Entity("Core.domain.Models.LoginTb", b =>
                {
                    b.Property<int>("LoginId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("LoginId");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoginId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int?>("CourseId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("NUMBER(1)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int?>("SessionId")
                        .IsRequired()
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("LoginId");

                    b.ToTable("LoginTb");
                });

            modelBuilder.Entity("Core.domain.Models.SesstionTB", b =>
                {
                    b.Property<int>("SesstionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("SesstionId");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SesstionId"), 1L, 1);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("NUMBER(1)");

                    b.Property<string>("Session")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TIMESTAMP(7)");

                    b.HasKey("SesstionId");

                    b.ToTable("SesstionTB");
                });

            modelBuilder.Entity("Core.domain.Models.TeacherTb", b =>
                {
                    b.Property<int>("TeacherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("TeacherId");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeacherId"), 1L, 1);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("NUMBER(1)");

                    b.Property<string>("TeacherName")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("TeacherId");

                    b.ToTable("TeacherTb");
                });
#pragma warning restore 612, 618
        }
    }
}