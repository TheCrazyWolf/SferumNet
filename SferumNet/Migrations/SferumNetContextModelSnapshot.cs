﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SferumNet.Database;

#nullable disable

namespace SferumNet.Migrations
{
    [DbContext(typeof(SferumNetContext))]
    partial class SferumNetContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("SferumNet.DbModels.Common.Job", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Delay")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.Property<long>("IdConversation")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("IdProfile")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsActiveForWeekend")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastExecuted")
                        .HasColumnType("TEXT");

                    b.Property<int>("MaxToExecute")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan>("TimeEnd")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("TimeStart")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TotalExecuted")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("IdProfile");

                    b.ToTable("Scenarios");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Job");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("SferumNet.DbModels.Data.WelcomeSentence", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("TEXT");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("WelcomeSentences");

                    b.HasDiscriminator<string>("Discriminator").HasValue("WelcomeSentence");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("SferumNet.DbModels.Services.Log", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<long?>("IdScenario")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("IdScenario");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("SferumNet.DbModels.Vk.VkProfile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("AccessTokenExpired")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RemixSid")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("VkProfiles");
                });

            modelBuilder.Entity("SferumNet.DbModels.Scenarios.FactJob", b =>
                {
                    b.HasBaseType("SferumNet.DbModels.Common.Job");

                    b.HasDiscriminator().HasValue("FactJob");
                });

            modelBuilder.Entity("SferumNet.DbModels.Scenarios.ScheduleJob", b =>
                {
                    b.HasBaseType("SferumNet.DbModels.Common.Job");

                    b.Property<bool>("IsAddedNextDay")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TypeSchedule")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("ScheduleJob");
                });

            modelBuilder.Entity("SferumNet.DbModels.Scenarios.WelcomeJob", b =>
                {
                    b.HasBaseType("SferumNet.DbModels.Common.Job");

                    b.HasDiscriminator().HasValue("WelcomeJob");
                });

            modelBuilder.Entity("SferumNet.DbModels.Data.FactSentences", b =>
                {
                    b.HasBaseType("SferumNet.DbModels.Data.WelcomeSentence");

                    b.HasDiscriminator().HasValue("FactSentences");
                });

            modelBuilder.Entity("SferumNet.DbModels.Common.Job", b =>
                {
                    b.HasOne("SferumNet.DbModels.Vk.VkProfile", "VkProfile")
                        .WithMany()
                        .HasForeignKey("IdProfile");

                    b.Navigation("VkProfile");
                });

            modelBuilder.Entity("SferumNet.DbModels.Services.Log", b =>
                {
                    b.HasOne("SferumNet.DbModels.Common.Job", "Scenario")
                        .WithMany()
                        .HasForeignKey("IdScenario")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Scenario");
                });
#pragma warning restore 612, 618
        }
    }
}
