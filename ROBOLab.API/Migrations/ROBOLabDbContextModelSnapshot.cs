﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ROBOLab.API;

namespace ROBOLab.API.Migrations
{
    [DbContext(typeof(ROBOLabDbContext))]
    partial class ROBOLabDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("ROBOLab.Core.Models.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DeviceTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DeviceTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Devices");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DeviceTypeId = 1,
                            Name = "Test device 1 (SmartTerra)",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            DeviceTypeId = 2,
                            Name = "RoboArm1",
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            DeviceTypeId = 1,
                            Name = "Test device 2 (SmartTerra)",
                            UserId = 2
                        },
                        new
                        {
                            Id = 4,
                            DeviceTypeId = 3,
                            Name = "Test device 2 (SmartTerra)",
                            UserId = 3
                        });
                });

            modelBuilder.Entity("ROBOLab.Core.Models.DeviceJob", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("DeviceId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Done")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("ExecutionTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("JobId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.HasIndex("JobId");

                    b.ToTable("DeviceJobs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Body = "#FF6611",
                            CreatedDate = new DateTime(2021, 7, 28, 3, 15, 12, 395, DateTimeKind.Local).AddTicks(9304),
                            DeviceId = 1,
                            Done = false,
                            JobId = 1
                        },
                        new
                        {
                            Id = 2,
                            Body = "",
                            CreatedDate = new DateTime(2021, 7, 28, 3, 15, 12, 399, DateTimeKind.Local).AddTicks(4206),
                            DeviceId = 1,
                            Done = false,
                            JobId = 2
                        });
                });

            modelBuilder.Entity("ROBOLab.Core.Models.DeviceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("DeviceTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "SmartTerra"
                        },
                        new
                        {
                            Id = 2,
                            Name = "RoboArm(Arexx RA-1-PRO)"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Dobot Magician V2"
                        });
                });

            modelBuilder.Entity("ROBOLab.Core.Models.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("DeviceTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Properties")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DeviceTypeId");

                    b.ToTable("Jobs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Turn on the LED strip and set color of the LEDs .",
                            DeviceTypeId = 1,
                            Name = "TurnOnLED",
                            Properties = ""
                        },
                        new
                        {
                            Id = 2,
                            Description = "Turn off the LED strip.",
                            DeviceTypeId = 1,
                            Name = "TurnOffLED",
                            Properties = ""
                        },
                        new
                        {
                            Id = 3,
                            Description = "Turn on the water pump for given period of time.",
                            DeviceTypeId = 1,
                            Name = "TurnOnWaterPump",
                            Properties = ""
                        },
                        new
                        {
                            Id = 4,
                            Description = "Move the teddy bear to a specific place.",
                            DeviceTypeId = 2,
                            Name = "MoveTeddyBear",
                            Properties = ""
                        },
                        new
                        {
                            Id = 5,
                            Description = "Pour water into the cube for given period of time.",
                            DeviceTypeId = 2,
                            Name = "FillCubeWithWater",
                            Properties = ""
                        },
                        new
                        {
                            Id = 6,
                            Description = "Run the provided sequence of angles.",
                            DeviceTypeId = 2,
                            Name = "RunAnySequence",
                            Properties = ""
                        });
                });

            modelBuilder.Entity("ROBOLab.Core.Models.Mode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DeviceId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("Modes");
                });

            modelBuilder.Entity("ROBOLab.Core.Models.Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("DeviceTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsMode")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ModeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DeviceTypeId");

                    b.HasIndex("ModeId");

                    b.ToTable("Properties");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Body = "type: int, min: 0, max: 180",
                            DeviceTypeId = 2,
                            IsMode = false,
                            Name = "Angle Of First Channel"
                        },
                        new
                        {
                            Id = 2,
                            Body = "type: int, min: 0, max: 180",
                            DeviceTypeId = 2,
                            IsMode = false,
                            Name = "Angle Of Second Channel"
                        },
                        new
                        {
                            Id = 3,
                            Body = "type: int, min: 0, max: 180",
                            DeviceTypeId = 2,
                            IsMode = false,
                            Name = "Angle Of Third Channel"
                        },
                        new
                        {
                            Id = 4,
                            Body = "type: int, min: 0, max: 180",
                            DeviceTypeId = 2,
                            IsMode = false,
                            Name = "Angle Of Fourth Channel"
                        },
                        new
                        {
                            Id = 5,
                            Body = "type: int, min: 0, max: 180",
                            DeviceTypeId = 2,
                            IsMode = false,
                            Name = "Angle Of Fifth Channel"
                        },
                        new
                        {
                            Id = 6,
                            Body = "type: int, min: 0, max: 180",
                            DeviceTypeId = 2,
                            IsMode = false,
                            Name = "Angle Of Sixth Channel"
                        },
                        new
                        {
                            Id = 7,
                            Body = "type: double, min: 0, max: 200",
                            DeviceTypeId = 3,
                            IsMode = false,
                            Name = "Temperature"
                        });
                });

            modelBuilder.Entity("ROBOLab.Core.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "buuu.email@gmail.com",
                            Login = "ola",
                            Password = "pass1"
                        },
                        new
                        {
                            Id = 2,
                            Email = "daniel.email@gmail.com",
                            Login = "daniel",
                            Password = "pass2"
                        },
                        new
                        {
                            Id = 3,
                            Email = "roboLab.email@gmail.com",
                            Login = "RoboLab User",
                            Password = "pass3"
                        });
                });

            modelBuilder.Entity("ROBOLab.Core.Models.Value", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("DeviceId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DeviceJobId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PropertyId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Val")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.HasIndex("DeviceJobId");

                    b.HasIndex("PropertyId");

                    b.ToTable("Values");
                });

            modelBuilder.Entity("ROBOLab.Core.Models.Device", b =>
                {
                    b.HasOne("ROBOLab.Core.Models.DeviceType", "DeviceType")
                        .WithMany("Devices")
                        .HasForeignKey("DeviceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ROBOLab.Core.Models.User", "User")
                        .WithMany("Devices")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeviceType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ROBOLab.Core.Models.DeviceJob", b =>
                {
                    b.HasOne("ROBOLab.Core.Models.Device", "Device")
                        .WithMany("DeviceJob")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ROBOLab.Core.Models.Job", "Job")
                        .WithMany("DeviceJobs")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("ROBOLab.Core.Models.Job", b =>
                {
                    b.HasOne("ROBOLab.Core.Models.DeviceType", "DeviceType")
                        .WithMany("Jobs")
                        .HasForeignKey("DeviceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeviceType");
                });

            modelBuilder.Entity("ROBOLab.Core.Models.Mode", b =>
                {
                    b.HasOne("ROBOLab.Core.Models.Device", "Device")
                        .WithMany("Modes")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("ROBOLab.Core.Models.Property", b =>
                {
                    b.HasOne("ROBOLab.Core.Models.DeviceType", "DeviceType")
                        .WithMany("Properties")
                        .HasForeignKey("DeviceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ROBOLab.Core.Models.Mode", "Mode")
                        .WithMany("Properties")
                        .HasForeignKey("ModeId");

                    b.Navigation("DeviceType");

                    b.Navigation("Mode");
                });

            modelBuilder.Entity("ROBOLab.Core.Models.Value", b =>
                {
                    b.HasOne("ROBOLab.Core.Models.Device", "Device")
                        .WithMany("Values")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ROBOLab.Core.Models.DeviceJob", "DeviceJob")
                        .WithMany()
                        .HasForeignKey("DeviceJobId");

                    b.HasOne("ROBOLab.Core.Models.Property", "Property")
                        .WithMany("Values")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("DeviceJob");

                    b.Navigation("Property");
                });

            modelBuilder.Entity("ROBOLab.Core.Models.Device", b =>
                {
                    b.Navigation("DeviceJob");

                    b.Navigation("Modes");

                    b.Navigation("Values");
                });

            modelBuilder.Entity("ROBOLab.Core.Models.DeviceType", b =>
                {
                    b.Navigation("Devices");

                    b.Navigation("Jobs");

                    b.Navigation("Properties");
                });

            modelBuilder.Entity("ROBOLab.Core.Models.Job", b =>
                {
                    b.Navigation("DeviceJobs");
                });

            modelBuilder.Entity("ROBOLab.Core.Models.Mode", b =>
                {
                    b.Navigation("Properties");
                });

            modelBuilder.Entity("ROBOLab.Core.Models.Property", b =>
                {
                    b.Navigation("Values");
                });

            modelBuilder.Entity("ROBOLab.Core.Models.User", b =>
                {
                    b.Navigation("Devices");
                });
#pragma warning restore 612, 618
        }
    }
}
