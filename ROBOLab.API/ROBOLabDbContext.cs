using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
