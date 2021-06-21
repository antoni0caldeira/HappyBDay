﻿// <auto-generated />
using System;
using HappyBDay.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HappyBDay.Migrations
{
    [DbContext(typeof(HappyBDayContext))]
    partial class HappyBDayContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HappyBDay.Models.Consultants", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConsultantNumber")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Consultant_Number");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("date")
                        .HasColumnName("Date_Of_Birth");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Gender")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<int>("IdDepartments")
                        .HasColumnType("int")
                        .HasColumnName("Id_Departments");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("IdDepartments");

                    b.ToTable("Consultants");
                });

            modelBuilder.Entity("HappyBDay.Models.Departments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("HappyBDay.Models.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Profile");
                });

            modelBuilder.Entity("HappyBDay.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("IdProfile")
                        .HasColumnType("int")
                        .HasColumnName("Id_Profile");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("IdProfile");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HappyBDay.Models.Consultants", b =>
                {
                    b.HasOne("HappyBDay.Models.Departments", "IdDepartmentsNavigation")
                        .WithMany("Consultants")
                        .HasForeignKey("IdDepartments")
                        .HasConstraintName("FK_Consultants_Departments")
                        .IsRequired();

                    b.Navigation("IdDepartmentsNavigation");
                });

            modelBuilder.Entity("HappyBDay.Models.Users", b =>
                {
                    b.HasOne("HappyBDay.Models.Profile", "IdProfileNavigation")
                        .WithMany("Users")
                        .HasForeignKey("IdProfile")
                        .HasConstraintName("FK_Users_Profile")
                        .IsRequired();

                    b.Navigation("IdProfileNavigation");
                });

            modelBuilder.Entity("HappyBDay.Models.Departments", b =>
                {
                    b.Navigation("Consultants");
                });

            modelBuilder.Entity("HappyBDay.Models.Profile", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
