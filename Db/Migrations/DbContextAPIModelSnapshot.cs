﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Onlineshop.Db;

#nullable disable

namespace SibersTest.Migrations
{
    [DbContext(typeof(DbContextAPI))]
    partial class DbContextAPIModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SibersTest.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PositionTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("employees");
                });

            modelBuilder.Entity("SibersTest.Models.EmployeeProject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("EmployeeId")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ProjectId");

                    b.ToTable("employee_projects");
                });

            modelBuilder.Entity("SibersTest.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClientCompany")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ExecutingCompany")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Priority")
                        .HasColumnType("integer");

                    b.Property<int?>("ProjectManagerId")
                        .HasColumnType("integer");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ProjectManagerId");

                    b.ToTable("projects");
                });

            modelBuilder.Entity("SibersTest.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ExecutorId")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ExecutorId");

                    b.HasIndex("ProjectId");

                    b.ToTable("tasks");
                });

            modelBuilder.Entity("SibersTest.Models.EmployeeProject", b =>
                {
                    b.HasOne("SibersTest.Models.Employee", "Employee")
                        .WithMany("EmployeeProjects")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SibersTest.Models.Project", "Project")
                        .WithMany("EmployeeProjects")
                        .HasForeignKey("ProjectId");

                    b.Navigation("Employee");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("SibersTest.Models.Project", b =>
                {
                    b.HasOne("SibersTest.Models.Employee", "ProjectManager")
                        .WithMany()
                        .HasForeignKey("ProjectManagerId");

                    b.Navigation("ProjectManager");
                });

            modelBuilder.Entity("SibersTest.Models.Task", b =>
                {
                    b.HasOne("SibersTest.Models.Employee", "Author")
                        .WithMany("Tasks")
                        .HasForeignKey("AuthorId");

                    b.HasOne("SibersTest.Models.EmployeeProject", "Executor")
                        .WithMany("Tasks")
                        .HasForeignKey("ExecutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SibersTest.Models.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId");

                    b.Navigation("Author");

                    b.Navigation("Executor");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("SibersTest.Models.Employee", b =>
                {
                    b.Navigation("EmployeeProjects");

                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("SibersTest.Models.EmployeeProject", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("SibersTest.Models.Project", b =>
                {
                    b.Navigation("EmployeeProjects");

                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
