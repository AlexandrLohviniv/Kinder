﻿// <auto-generated />
using System;
using KinderApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KinderApi.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210520193210_likeTableUniqConstraintsAdd")]
    partial class likeTableUniqConstraintsAdd
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KinderApi.Models.BannedUsers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("LastBanDay")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("BannedUsers");
                });

            modelBuilder.Entity("KinderApi.Models.Complaint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ReceiverId")
                        .HasColumnType("int");

                    b.Property<int?>("SenderId")
                        .HasColumnType("int");

                    b.Property<int>("complaint")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Complaints");
                });

            modelBuilder.Entity("KinderApi.Models.Image", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImagePublicId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsMain")
                        .HasColumnType("bit");

                    b.Property<int?>("Userid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("Userid");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("KinderApi.Models.Like", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ReceiverId")
                        .HasColumnType("int");

                    b.Property<int?>("SenderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("sendingTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId", "ReceiverId")
                        .IsUnique()
                        .HasFilter("[SenderId] IS NOT NULL AND [ReceiverId] IS NOT NULL");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("KinderApi.Models.Message", b =>
                {
                    b.Property<Guid>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("ReceiverId")
                        .HasColumnType("int");

                    b.Property<int?>("SenderId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isRead")
                        .HasColumnType("bit");

                    b.Property<DateTime>("sendingTime")
                        .HasColumnType("datetime2");

                    b.HasKey("MessageId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("KinderApi.Models.Preference", b =>
                {
                    b.Property<Guid>("PreferenceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BabyRate")
                        .HasColumnType("int");

                    b.Property<int>("DrinkingRate")
                        .HasColumnType("int");

                    b.Property<int>("HeightRate")
                        .HasColumnType("int");

                    b.Property<int>("PetsRate")
                        .HasColumnType("int");

                    b.Property<int>("RelationshipRate")
                        .HasColumnType("int");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.Property<int>("SmokeRate")
                        .HasColumnType("int");

                    b.HasKey("PreferenceId");

                    b.ToTable("Prefernces");
                });

            modelBuilder.Entity("KinderApi.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AboutMe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Coordinate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBith")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastSeen")
                        .HasColumnType("datetime2");

                    b.Property<string>("NickName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PreferenceUser", b =>
                {
                    b.Property<Guid>("PreferencesPreferenceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("PreferencesPreferenceId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("PreferenceUser");
                });

            modelBuilder.Entity("KinderApi.Models.BannedUsers", b =>
                {
                    b.HasOne("KinderApi.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("KinderApi.Models.Complaint", b =>
                {
                    b.HasOne("KinderApi.Models.User", "Receiver")
                        .WithMany("ReceivedComplaints")
                        .HasForeignKey("ReceiverId");

                    b.HasOne("KinderApi.Models.User", "Sender")
                        .WithMany("SentComplaints")
                        .HasForeignKey("SenderId");

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("KinderApi.Models.Image", b =>
                {
                    b.HasOne("KinderApi.Models.User", "User")
                        .WithMany("Images")
                        .HasForeignKey("Userid");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KinderApi.Models.Like", b =>
                {
                    b.HasOne("KinderApi.Models.User", "Receiver")
                        .WithMany("ReceivedLikes")
                        .HasForeignKey("ReceiverId");

                    b.HasOne("KinderApi.Models.User", "Sender")
                        .WithMany("SentLikes")
                        .HasForeignKey("SenderId");

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("KinderApi.Models.Message", b =>
                {
                    b.HasOne("KinderApi.Models.User", "Receiver")
                        .WithMany("ReceivedMessages")
                        .HasForeignKey("ReceiverId");

                    b.HasOne("KinderApi.Models.User", "Sender")
                        .WithMany("SentMessages")
                        .HasForeignKey("SenderId");

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("PreferenceUser", b =>
                {
                    b.HasOne("KinderApi.Models.Preference", null)
                        .WithMany()
                        .HasForeignKey("PreferencesPreferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KinderApi.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KinderApi.Models.User", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("ReceivedComplaints");

                    b.Navigation("ReceivedLikes");

                    b.Navigation("ReceivedMessages");

                    b.Navigation("SentComplaints");

                    b.Navigation("SentLikes");

                    b.Navigation("SentMessages");
                });
#pragma warning restore 612, 618
        }
    }
}