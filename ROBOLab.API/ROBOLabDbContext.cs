using System;
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
            var testDevType = new DeviceType
            {
                Id = 2,
                Name = "Device type test"
            };

            modelBuilder.Entity<DeviceType>().HasData(new DeviceType[]
            {
                smartTerraDevType,
                testDevType
            });

            // jobs

            modelBuilder.Entity<Job>().HasData(new object[]
            {
                new
                {
                    Id = 1,
                    Name = "TurnOnLED",
                    DeviceTypeId = smartTerraDevType.Id,
                    Properties = "",
                    Description = "Turn on the LED strip and set color of the LEDs ."
                },
                new
                {
                    Id = 2,
                    Name = "TurnOffLED",
                    DeviceTypeId = smartTerraDevType.Id,
                    Properties = "",
                    Description = "Turn off the LED strip."
                },
                new
                {
                    Id = 3,
                    Name = "TurnOnWaterPump",
                    DeviceTypeId = smartTerraDevType.Id,
                    Properties = "",
                    Description = "Turn on the water pump for given period of time."
                }
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

            modelBuilder.Entity<Device>().HasData(new Device[]
            {
                new Device
                {
                    Id = 1,
                    DeviceTypeId = smartTerraDevType.Id,
                    Name = "Test device 1 (SmartTerra)",
                    UserId = user1.Id,
                },
                new Device
                {
                    Id = 2,
                    DeviceTypeId = smartTerraDevType.Id,
                    Name = "Test device 2 (SmartTerra)",
                    UserId = user2.Id,
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
