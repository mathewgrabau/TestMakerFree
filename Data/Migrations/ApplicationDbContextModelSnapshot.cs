﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TestMakerFreeWebApp.Data;

namespace TestMakerFreeWebApp.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TestMakerFreeWebApp.Data.Models.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("Flags");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<string>("Notes");

                    b.Property<int>("QuestionId");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.Property<int>("Type");

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("TestMakerFreeWebApp.Data.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("DisplayName");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<int>("Flags");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<string>("Notes");

                    b.Property<int>("Type");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TestMakerFreeWebApp.Data.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("Flags");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<string>("Notes");

                    b.Property<int>("QuizId");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("QuizId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("TestMakerFreeWebApp.Data.Models.Quiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<int>("Flags");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<string>("Notes");

                    b.Property<string>("Text");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("Type");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.Property<int>("ViewCount");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Quizzes");
                });

            modelBuilder.Entity("TestMakerFreeWebApp.Data.Models.Result", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("Flags");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<int?>("MaxValue");

                    b.Property<int?>("MinValue");

                    b.Property<string>("Notes");

                    b.Property<int>("QuizId");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("QuizId");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("TestMakerFreeWebApp.Data.Models.Answer", b =>
                {
                    b.HasOne("TestMakerFreeWebApp.Data.Models.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TestMakerFreeWebApp.Data.Models.Question", b =>
                {
                    b.HasOne("TestMakerFreeWebApp.Data.Models.Quiz", "Quiz")
                        .WithMany("Questions")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TestMakerFreeWebApp.Data.Models.Quiz", b =>
                {
                    b.HasOne("TestMakerFreeWebApp.Data.Models.ApplicationUser", "User")
                        .WithMany("Quizzes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TestMakerFreeWebApp.Data.Models.Result", b =>
                {
                    b.HasOne("TestMakerFreeWebApp.Data.Models.Quiz", "Quiz")
                        .WithMany("Results")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
