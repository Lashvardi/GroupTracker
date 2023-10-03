﻿// <auto-generated />
using System;
using GroupTracker.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GroupTracker.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230921100247_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GroupTracker.DTOs.GroupLectureSession.GroupLectureSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("LectureSessionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("LectureSessionId");

                    b.ToTable("GroupLectureSessions");
                });

            modelBuilder.Entity("GroupTracker.Models.AlternateWeek", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsMyturn")
                        .HasColumnType("bit");

                    b.Property<int>("LectureSessionId")
                        .HasColumnType("int");

                    b.Property<string>("WeekNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LectureSessionId");

                    b.ToTable("AlternateWeeks");
                });

            modelBuilder.Entity("GroupTracker.Models.FileAttachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SyllabusTopicId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SyllabusTopicId");

                    b.ToTable("FileAttachment");
                });

            modelBuilder.Entity("GroupTracker.Models.LectureSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Auditorium")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Day")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAlternate")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOnline")
                        .HasColumnType("bit");

                    b.Property<string>("Time")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LectureSessions");
                });

            modelBuilder.Entity("GroupTracker.Models.Lecturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Companies")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerificationCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Lecturers");
                });

            modelBuilder.Entity("GroupTracker.Models.LecturerGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CurrentSyllabusTopicId")
                        .HasColumnType("int");

                    b.Property<string>("Grade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GroupName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LecturerId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CurrentSyllabusTopicId");

                    b.HasIndex("LecturerId");

                    b.ToTable("LecturerGroups");
                });

            modelBuilder.Entity("GroupTracker.Models.SyllabusTopic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LecturerGroupId")
                        .HasColumnType("int");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LecturerGroupId");

                    b.ToTable("SyllabusTopics");
                });

            modelBuilder.Entity("GroupTracker.DTOs.GroupLectureSession.GroupLectureSession", b =>
                {
                    b.HasOne("GroupTracker.Models.LecturerGroup", "Group")
                        .WithMany("GroupLectureSessions")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GroupTracker.Models.LectureSession", "LectureSession")
                        .WithMany("GroupLectureSessions")
                        .HasForeignKey("LectureSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("LectureSession");
                });

            modelBuilder.Entity("GroupTracker.Models.AlternateWeek", b =>
                {
                    b.HasOne("GroupTracker.Models.LectureSession", "LectureSession")
                        .WithMany("AlternateWeeks")
                        .HasForeignKey("LectureSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LectureSession");
                });

            modelBuilder.Entity("GroupTracker.Models.FileAttachment", b =>
                {
                    b.HasOne("GroupTracker.Models.SyllabusTopic", "SyllabusTopic")
                        .WithMany("FileAttachments")
                        .HasForeignKey("SyllabusTopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SyllabusTopic");
                });

            modelBuilder.Entity("GroupTracker.Models.LecturerGroup", b =>
                {
                    b.HasOne("GroupTracker.Models.SyllabusTopic", "CurrentSyllabusTopic")
                        .WithMany()
                        .HasForeignKey("CurrentSyllabusTopicId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GroupTracker.Models.Lecturer", "Lecturer")
                        .WithMany("LecturerGroups")
                        .HasForeignKey("LecturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CurrentSyllabusTopic");

                    b.Navigation("Lecturer");
                });

            modelBuilder.Entity("GroupTracker.Models.SyllabusTopic", b =>
                {
                    b.HasOne("GroupTracker.Models.LecturerGroup", "LecturerGroup")
                        .WithMany("SyllabusTopics")
                        .HasForeignKey("LecturerGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LecturerGroup");
                });

            modelBuilder.Entity("GroupTracker.Models.LectureSession", b =>
                {
                    b.Navigation("AlternateWeeks");

                    b.Navigation("GroupLectureSessions");
                });

            modelBuilder.Entity("GroupTracker.Models.Lecturer", b =>
                {
                    b.Navigation("LecturerGroups");
                });

            modelBuilder.Entity("GroupTracker.Models.LecturerGroup", b =>
                {
                    b.Navigation("GroupLectureSessions");

                    b.Navigation("SyllabusTopics");
                });

            modelBuilder.Entity("GroupTracker.Models.SyllabusTopic", b =>
                {
                    b.Navigation("FileAttachments");
                });
#pragma warning restore 612, 618
        }
    }
}