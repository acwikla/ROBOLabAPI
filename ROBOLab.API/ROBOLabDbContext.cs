﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using ROBOLab.Core.Models;

namespace ROBOLab.API
{
    public class ROBOLabDbContext : DbContext
    {
        public ROBOLabDbContext()
        {

        }

        public ROBOLabDbContext(DbContextOptions<ROBOLabDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // device types
            var smartTerraDevType = new DeviceType
            {
                Id = 1,
                Name = "SmartTerra"
            };
            var roboArmDevType = new DeviceType
            {
                Id = 2,
                Name = "RoboArm(Arexx RA-1-PRO)"
            };
            //roboLab
            var dobotMagicianV2DevType = new DeviceType
            {
                Id = 3,
                Name = "Dobot Magician V2"
            };
            var roboLabPressDevType = new DeviceType
            {
                Id = 4,
                Name = "ROBOLab Press"
            };

            modelBuilder.Entity<DeviceType>().HasData(new DeviceType[]
            {
                smartTerraDevType,
                roboArmDevType,
                dobotMagicianV2DevType,
                roboLabPressDevType
            });

            // properties
            modelBuilder.Entity<Property>().HasData(new Property []
            {
                new Property{
                    Id = 1,
                    Name = "Angle Of First Channel",
                    Body = "type: int, min: 0, max: 180",
                    DeviceTypeId = roboArmDevType.Id
                },new Property{
                    Id = 2,
                    Name = "Angle Of Second Channel",
                    Body = "type: int, min: 0, max: 180",
                    DeviceTypeId = roboArmDevType.Id
                },new Property{
                    Id = 3,
                    Name = "Angle Of Third Channel",
                    Body = "type: int, min: 0, max: 180",
                    DeviceTypeId = roboArmDevType.Id
                },new Property{
                    Id = 4,
                    Name = "Angle Of Fourth Channel",
                    Body = "type: int, min: 0, max: 180",
                    DeviceTypeId = roboArmDevType.Id
                },new Property{
                    Id = 5,
                    Name = "Angle Of Fifth Channel",
                    Body = "type: int, min: 0, max: 180",
                    DeviceTypeId = roboArmDevType.Id
                },new Property{
                    Id = 6,
                    Name = "Angle Of Sixth Channel",
                    Body = "type: int, min: 0, max: 180",
                    DeviceTypeId = roboArmDevType.Id
                },
                new Property{
                    Id = 100,
                    Name = "Angle",
                    Body = "type: float, min: null, max: null",
                    DeviceTypeId = dobotMagicianV2DevType.Id,
                },
                new Property{
                    Id = 120,
                    Name = "Pressure force",
                    Body = "type: float, min: 0, max: 10000000",
                    DeviceTypeId = roboLabPressDevType.Id,
                },
            });

            // jobs
            var jobTurnOnLED = new
            {
                Id = 1,
                Name = "TurnOnLED",
                DeviceTypeId = smartTerraDevType.Id,
                Properties = "",
                Description = "Turn on the LED strip and set color of the LEDs ."
            };
            var jobTurnOffLED = new
            {
                Id = 2,
                Name = "TurnOffLED",
                DeviceTypeId = smartTerraDevType.Id,
                Properties = "",
                Description = "Turn off the LED strip."
            };
            var jobTurnOnWaterPump = new
            {
                Id = 3,
                Name = "TurnOnWaterPump",
                DeviceTypeId = smartTerraDevType.Id,
                Properties = "",
                Description = "Turn on the water pump for given period of time."
            };
            //--------------------
            var jobMoveTeddyBear = new
            {
                Id = 4,
                Name = "MoveTeddyBear",
                DeviceTypeId = roboArmDevType.Id,
                Properties = "",
                Description = "Move the teddy bear to a specific place."
            };
            var jobFillCubeWithWater = new
            {
                Id = 5,
                Name = "FillCubeWithWater",
                DeviceTypeId = roboArmDevType.Id,
                Properties = "",
                Description = "Pour water into the cube for given period of time."
            };
            var jobRunAnySequence = new
            {
                Id = 6,
                Name = "RunAnySequence",
                DeviceTypeId = roboArmDevType.Id,
                Properties = "",
                Description = "Run the provided sequence of angles."
            };
            //-------RoboLab------
            var jobMagicanPutTheSampleOnThePress = new
            {
                Id = 100,
                Name = "Put the sample on the press",
                DeviceTypeId = dobotMagicianV2DevType.Id,
                Properties = "",
                Description = "Put the sample on the press."
            };
            var jobPressSqueezeTheSample = new
            {
                Id = 120,
                Name = "Squeeze The Sample",
                DeviceTypeId = roboLabPressDevType.Id,
                Properties = "",
                Description = "Squeeze the sample."
            };
            var jobPressReleaseTheSample = new
            {
                Id = 121,
                Name = "Release The Sample",
                DeviceTypeId = roboLabPressDevType.Id,
                Properties = "",
                Description = "Release the sample."
            };

            modelBuilder.Entity<Job>().HasData(new object[]
            {
                jobTurnOnLED,
                jobTurnOffLED,
                jobTurnOnWaterPump,
                jobMoveTeddyBear,
                jobFillCubeWithWater,
                jobRunAnySequence,
                jobMagicanPutTheSampleOnThePress,
                jobPressSqueezeTheSample,
                jobPressReleaseTheSample
            });


            // users
            var user1 = new User
            {
                Id = 1,
                Email = "buuu.email@gmail.com",
                Login = "ola",
                Password = "pass1"
            };
            var user2 = new User
            {
                Id = 2,
                Email = "daniel.email@gmail.com",
                Login = "daniel",
                Password = "pass2"
            };
            //roboLab
            var user3 = new User
            {
                Id = 3,
                Email = "roboLab.email@gmail.com",
                Login = "RoboLab User",
                Password = "pass3"
            };

            modelBuilder.Entity<User>().HasData(new User[]
            {
                user1,
                user2,
                user3
            });


            // devices
            var smartTerraDev1 = new Device
            {
                Id = 1,
                DeviceTypeId = smartTerraDevType.Id,
                Name = "Test device 1 (SmartTerra)",
                UserId = user1.Id,
            };
            var roboArmDev1 = new Device
            {
                Id = 2,
                DeviceTypeId = roboArmDevType.Id,
                Name = "RoboArm1",
                UserId = user1.Id,
            };
            var smartTerraDev2 = new Device
            {
                Id = 3,
                DeviceTypeId = smartTerraDevType.Id,
                Name = "Test device 2 (SmartTerra)",
                UserId = user2.Id,
            };
            //robolab
            var dobotMagicianDev = new Device
            {
                Id = 100,
                DeviceTypeId = dobotMagicianV2DevType.Id,
                Name = "Dobot Magician V2 (RoboArm)",
                UserId = user3.Id,
            };
            var roboLabPressDev = new Device
            {
                Id = 120,
                DeviceTypeId = roboLabPressDevType.Id,
                Name = "ROBOLab Press",
                UserId = user3.Id,
            };

            modelBuilder.Entity<Device>().HasData(new Device[]
            {
                smartTerraDev1,
                roboArmDev1,
                smartTerraDev2,
                dobotMagicianDev,
                roboLabPressDev
            });


            // device jobs
            modelBuilder.Entity<DeviceJob>().HasData(new DeviceJob[]
            {
                // smart terra: led on
                new DeviceJob
                {
                    Id = 1,
                    CreatedDate = DateTime.Now,
                    DeviceId = smartTerraDev1.Id,
                    JobId = jobTurnOnLED.Id,
                    Done = false,
                    ExecutionTime = null,
                    Body = "#FF6611",
                },

                // smart terra: led off
                new DeviceJob
                {
                    Id = 2,
                    CreatedDate = DateTime.Now,
                    DeviceId = smartTerraDev1.Id,
                    JobId = jobTurnOffLED.Id,
                    Done = false,
                    ExecutionTime = null,
                    Body = "",
                },

                // magician arm v2: put the sample on the press
                new DeviceJob
                {
                    Id = 3,
                    CreatedDate = DateTime.Now,
                    DeviceId = dobotMagicianDev.Id,
                    JobId = jobMagicanPutTheSampleOnThePress.Id,
                    Done = false,
                    ExecutionTime = null,
                    Body = "",
                },

                // robo lab press: squeeze the sample
                new DeviceJob
                {
                    Id = 4,
                    CreatedDate = DateTime.Now,
                    DeviceId = roboLabPressDev.Id,
                    JobId = jobPressSqueezeTheSample.Id,
                    Done = false,
                    ExecutionTime = null,
                    Body = "",
                },

                // robo lab press: release the sample
                new DeviceJob
                {
                    Id = 5,
                    CreatedDate = DateTime.Now,
                    DeviceId = roboLabPressDev.Id,
                    JobId = jobPressReleaseTheSample.Id,
                    Done = false,
                    ExecutionTime = null,
                    Body = "",
                }
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Value> Values { get; set; }
        public DbSet<Mode> Modes { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<DeviceJob> DeviceJobs { get; set; }
    }
}
