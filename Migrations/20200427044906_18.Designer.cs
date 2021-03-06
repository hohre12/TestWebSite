﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestWebSIte.Data;

namespace TestWebSIte.Migrations
{
    [DbContext(typeof(BoardDbContext))]
    [Migration("20200427044906_18")]
    partial class _18
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TestWebSIte.Models.Answer", b =>
                {
                    b.Property<int>("AnswerNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AnswerContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AnswerDay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryChoice")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InquireNo")
                        .HasColumnType("int");

                    b.Property<string>("QuestionDay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionMonth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Questions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserNo")
                        .HasColumnType("int");

                    b.HasKey("AnswerNo");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("TestWebSIte.Models.ApprovedOrder", b =>
                {
                    b.Property<int>("ApprovalNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BoardContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BoardNo")
                        .HasColumnType("int");

                    b.Property<string>("BoardTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderNo")
                        .HasColumnType("int");

                    b.Property<int>("UserNo")
                        .HasColumnType("int");

                    b.HasKey("ApprovalNo");

                    b.ToTable("ApprovedOrders");
                });

            modelBuilder.Entity("TestWebSIte.Models.Board", b =>
                {
                    b.Property<int>("BoardNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BoardContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BoardTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Modidate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Regdate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserNo")
                        .HasColumnType("int");

                    b.Property<int>("ViewCnt")
                        .HasColumnType("int");

                    b.HasKey("BoardNo");

                    b.HasIndex("UserNo");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("TestWebSIte.Models.Faq", b =>
                {
                    b.Property<int>("FaqNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryChoice")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FaqAnswer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FaqTItle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Regdate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserNo")
                        .HasColumnType("int");

                    b.HasKey("FaqNo");

                    b.ToTable("Faqs");
                });

            modelBuilder.Entity("TestWebSIte.Models.Inquire", b =>
                {
                    b.Property<int>("InquireNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryChoice")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionDay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionMonth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Questions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserNo")
                        .HasColumnType("int");

                    b.HasKey("InquireNo");

                    b.ToTable("Inquires");
                });

            modelBuilder.Entity("TestWebSIte.Models.Notice", b =>
                {
                    b.Property<int>("NoticeNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NoticeContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoticeTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Regdate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserNo")
                        .HasColumnType("int");

                    b.Property<int>("ViewCnt")
                        .HasColumnType("int");

                    b.HasKey("NoticeNo");

                    b.ToTable("Notices");
                });

            modelBuilder.Entity("TestWebSIte.Models.Order", b =>
                {
                    b.Property<int>("OrderNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BoardContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BoardNo")
                        .HasColumnType("int");

                    b.Property<string>("BoardTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrderDay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrderMonth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrderYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserNo")
                        .HasColumnType("int");

                    b.HasKey("OrderNo");

                    b.HasIndex("BoardNo");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("TestWebSIte.Models.User", b =>
                {
                    b.Property<int>("UserNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SignUpDay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SignUpMonth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SignUpYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserIntro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPw")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserNo");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TestWebSIte.Models.Board", b =>
                {
                    b.HasOne("TestWebSIte.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TestWebSIte.Models.Order", b =>
                {
                    b.HasOne("TestWebSIte.Models.Board", "Board")
                        .WithMany()
                        .HasForeignKey("BoardNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
