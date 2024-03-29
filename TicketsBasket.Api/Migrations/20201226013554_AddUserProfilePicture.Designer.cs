﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TicketsBasket.Models.Data;

namespace TicketsBasket.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201226013554_AddUserProfilePicture")]
    partial class AddUserProfilePicture
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TicketsBasket.Models.Domain.Event", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CoverImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(265)")
                        .HasMaxLength(265);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TicketsCount")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<string>("UserProfileId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Views")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserProfileId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("TicketsBasket.Models.Domain.EventImage", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EventId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(265)")
                        .HasMaxLength(265);

                    b.Property<string>("ThumpUrl")
                        .HasColumnType("nvarchar(265)")
                        .HasMaxLength(265);

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("EventImages");
                });

            modelBuilder.Entity("TicketsBasket.Models.Domain.EventTag", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EventId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(265)")
                        .HasMaxLength(265);

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("EventTags");
                });

            modelBuilder.Entity("TicketsBasket.Models.Domain.JobApplcation", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ApplicationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("AppliedUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CvUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrganizerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AppliedUserId");

                    b.HasIndex("OrganizerId");

                    b.ToTable("JobApplcations");
                });

            modelBuilder.Entity("TicketsBasket.Models.Domain.Like", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("EventId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserProfileId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("TicketsBasket.Models.Domain.Ticket", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Barcode")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Discount")
                        .HasColumnType("int");

                    b.Property<string>("EventId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("FinalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Place")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserProfileId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("TicketsBasket.Models.Domain.UserProfile", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<bool>("IsOrganizer")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("TicketsBasket.Models.Domain.WishListEvent", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("EventId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserProfileId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("WishListEvents");
                });

            modelBuilder.Entity("TicketsBasket.Models.Domain.Event", b =>
                {
                    b.HasOne("TicketsBasket.Models.Domain.UserProfile", "UserProfile")
                        .WithMany("Events")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("TicketsBasket.Models.Domain.EventImage", b =>
                {
                    b.HasOne("TicketsBasket.Models.Domain.Event", "Event")
                        .WithMany("EventImages")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("TicketsBasket.Models.Domain.EventTag", b =>
                {
                    b.HasOne("TicketsBasket.Models.Domain.Event", "Event")
                        .WithMany("EventTags")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("TicketsBasket.Models.Domain.JobApplcation", b =>
                {
                    b.HasOne("TicketsBasket.Models.Domain.UserProfile", "AppliedUser")
                        .WithMany("SenApplications")
                        .HasForeignKey("AppliedUserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("TicketsBasket.Models.Domain.UserProfile", "Organizer")
                        .WithMany("RecievedApplications")
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("TicketsBasket.Models.Domain.Like", b =>
                {
                    b.HasOne("TicketsBasket.Models.Domain.Event", "Event")
                        .WithMany("Likes")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("TicketsBasket.Models.Domain.UserProfile", "UserProfile")
                        .WithMany("Likes")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("TicketsBasket.Models.Domain.Ticket", b =>
                {
                    b.HasOne("TicketsBasket.Models.Domain.Event", "Event")
                        .WithMany("Tickets")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("TicketsBasket.Models.Domain.UserProfile", "UserProfile")
                        .WithMany("Tickets")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("TicketsBasket.Models.Domain.WishListEvent", b =>
                {
                    b.HasOne("TicketsBasket.Models.Domain.Event", "Event")
                        .WithMany("WishListEvents")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("TicketsBasket.Models.Domain.UserProfile", "UserProfile")
                        .WithMany("WishListEvents")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.NoAction);
                });
#pragma warning restore 612, 618
        }
    }
}
