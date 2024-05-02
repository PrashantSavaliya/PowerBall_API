﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PowerBall.Data;

#nullable disable

namespace PowerBall.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240129101021_AllTables")]
    partial class AllTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PowerBall.Models.Draw", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DrawDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DrawId")
                        .HasColumnType("int");

                    b.Property<string>("FResult")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HasWinner")
                        .HasColumnType("int");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<double>("WinningAmount")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Draw");
                });

            modelBuilder.Entity("PowerBall.Models.Games", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GameNumber")
                        .HasColumnType("int");

                    b.Property<string>("MatchingNumbers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("TicketID")
                        .HasColumnType("int");

                    b.Property<int>("TicketsTicketID")
                        .HasColumnType("int");

                    b.Property<string>("Values")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("WinningAmount")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("TicketsTicketID");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("PowerBall.Models.RegisterLogin", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsLoggedIn")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("RegisterLogins");
                });

            modelBuilder.Entity("PowerBall.Models.Tickets", b =>
                {
                    b.Property<int>("TicketID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TicketID"));

                    b.Property<string>("BarCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DrawId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PlayerDateTime")
                        .HasColumnType("datetime2");

                    b.Property<double>("TotalAmount")
                        .HasColumnType("float");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TicketID");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("PowerBall.Models.Games", b =>
                {
                    b.HasOne("PowerBall.Models.Tickets", "Tickets")
                        .WithMany("Games")
                        .HasForeignKey("TicketsTicketID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("PowerBall.Models.Tickets", b =>
                {
                    b.Navigation("Games");
                });
#pragma warning restore 612, 618
        }
    }
}
