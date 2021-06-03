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

            modelBuilder.Entity<DeviceType>().HasData(new DeviceType[]
            {
                smartTerraDevType,
                roboArmDevType
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

            modelBuilder.Entity<Job>().HasData(new object[]
            {
                jobTurnOnLED,
                jobTurnOffLED,
                jobTurnOnWaterPump,
                jobMoveTeddyBear,
                jobFillCubeWithWater,
                jobRunAnySequence
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

            modelBuilder.Entity<User>().HasData(new User[]
            {
                user1,
                user2
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

            modelBuilder.Entity<Device>().HasData(new Device[]
            {
                smartTerraDev1,
                roboArmDev1,
                smartTerraDev2
            });


            // device jobs

            modelBuilder.Entity<DeviceJob>().HasData(new DeviceJob[]
            {
                // led on
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

                // led off
                new DeviceJob
                {
                    Id = 2,
                    CreatedDate = DateTime.Now,
                    DeviceId = smartTerraDev1.Id,
                    JobId = jobTurnOffLED.Id,
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
