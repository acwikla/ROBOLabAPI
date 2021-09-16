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
            var roboArmArexxDevType = new DeviceType
            {
                Id = 2,
                Name = "RoboArm(Arexx RA-1-PRO)"
            };
            //roboLab
            var roboArmDobotMagicianV2DevType = new DeviceType
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
                roboArmArexxDevType,
                roboArmDobotMagicianV2DevType,
                roboLabPressDevType
            });

            // properties
            modelBuilder.Entity<Property>().HasData(new Property []
            {
                new Property{
                    Id = 1,
                    Name = "Angle Of First Channel",
                    Body = "type: int, min: 0, max: 180",
                    DeviceTypeId = roboArmArexxDevType.Id
                },new Property{
                    Id = 2,
                    Name = "Angle Of Second Channel",
                    Body = "type: int, min: 0, max: 180",
                    DeviceTypeId = roboArmArexxDevType.Id
                },new Property{
                    Id = 3,
                    Name = "Angle Of Third Channel",
                    Body = "type: int, min: 0, max: 180",
                    DeviceTypeId = roboArmArexxDevType.Id
                },new Property{
                    Id = 4,
                    Name = "Angle Of Fourth Channel",
                    Body = "type: int, min: 0, max: 180",
                    DeviceTypeId = roboArmArexxDevType.Id
                },new Property{
                    Id = 5,
                    Name = "Angle Of Fifth Channel",
                    Body = "type: int, min: 0, max: 180",
                    DeviceTypeId = roboArmArexxDevType.Id
                },new Property{
                    Id = 6,
                    Name = "Angle Of Sixth Channel",
                    Body = "type: int, min: 0, max: 180",
                    DeviceTypeId = roboArmArexxDevType.Id
                },
                //------RoboLab press------
                new Property{
                    Id = 120,
                    Name = "Pressure force",
                    Body = "type: float, min: 0, max: 200",
                    DeviceTypeId = roboLabPressDevType.Id,
                },
                //------DobotMagician------
                new Property{
                    Id = 100,
                    Name = "X",
                    Body = "type: float",
                    DeviceTypeId = roboArmDobotMagicianV2DevType.Id,
                },
                new Property{
                    Id = 101,
                    Name = "Y",
                    Body = "type: float",
                    DeviceTypeId = roboArmDobotMagicianV2DevType.Id,
                },
                new Property{
                    Id = 102,
                    Name = "Z",
                    Body = "type: float",
                    DeviceTypeId = roboArmDobotMagicianV2DevType.Id,
                },
                new Property{
                    Id = 103,
                    Name = "R",
                    Body = "type: float",
                    DeviceTypeId = roboArmDobotMagicianV2DevType.Id,
                },
                new Property{
                    Id = 104,
                    Name = "Angle 1",
                    Body = "type: float",
                    DeviceTypeId = roboArmDobotMagicianV2DevType.Id,
                },
                new Property{
                    Id = 105,
                    Name = "Angle 2",
                    Body = "type: float",
                    DeviceTypeId = roboArmDobotMagicianV2DevType.Id,
                },
                new Property{
                    Id = 106,
                    Name = "Angle 3",
                    Body = "type: float",
                    DeviceTypeId = roboArmDobotMagicianV2DevType.Id,
                },
            });

            // jobs
            var jobTurnOnLED = new
            {
                Id = 1,
                Name = "TurnOnLED",
                DeviceTypeId = smartTerraDevType.Id,
                Properties = "name: color, type: Color",
                Description = "Turn on the LED strip and set color of the LEDs ."
            };
            var jobTurnOffLED = new
            {
                Id = 2,
                Name = "TurnOffLED",
                DeviceTypeId = smartTerraDevType.Id,
                Properties = "nan",
                Description = "Turn off the LED strip."
            };
            var jobTurnOnWaterPump = new
            {
                Id = 3,
                Name = "TurnOnWaterPump",
                DeviceTypeId = smartTerraDevType.Id,
                Properties = "name: workingTime, type: Time",
                Description = "Turn on the water pump for given period of time."
            };
            //--------------------
            var jobMoveTeddyBear = new
            {
                Id = 4,
                Name = "MoveTeddyBear",
                DeviceTypeId = roboArmArexxDevType.Id,
                Properties = "name: sixServoVal, type: int, min: 0, max: 5;",
                Description = "Move the teddy bear to a specific place."
            };
            var jobFillCubeWithWater = new
            {
                Id = 5,
                Name = "FillCubeWithWater",
                DeviceTypeId = roboArmArexxDevType.Id,
                Properties = "name: pouringTime, type: Time, min: 0",
                Description = "Pour water into the cube for given period of time."
            };
            var jobRunAnySequence = new
            {
                Id = 6,
                Name = "RunAnySequence",
                DeviceTypeId = roboArmArexxDevType.Id,
                Properties = "name: channels, type: table[int], min: 0, max: 12;",
                Description = "Run the provided sequence of angles."
            };
            var jobRunAnyDifferenceSequence = new
            {
                Id = 7,
                Name = "RunAnyDifferenceSequence",
                DeviceTypeId = roboArmArexxDevType.Id,
                Description = "Run any sequence with angle difference.",
                Properties = "name: channels, type: table[int], min: 0, max: 5; name: angleDifferences, type: table[int], min: 0, max: 180",
            };
            var jobSetInitialPose = new
            {
                Id = 8,
                Name = "SetInitialPose",
                DeviceTypeId = roboArmArexxDevType.Id,
                Description = "Set initial robo arm pose.",
                Properties = "",
            };
            //-------RoboLab------
            var jobMagicanPutTheSampleOnThePress = new          // arm
            {
                Id = 100,
                Name = "Put the sample on the press",
                DeviceTypeId = roboArmDobotMagicianV2DevType.Id,
                Properties = "name: X, type: float; name: Y, type: float; name: Z, type: float",
                Description = "Put the sample on the press."
            };
            var jobPressSqueezeTheSample = new                  // press
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
            var jobPressCalibration = new
            {
                Id = 122,
                Name = "Press calibration",
                DeviceTypeId = roboLabPressDevType.Id,
                Properties = "name: stampHeight, type: float, min: 0, max: 6",
                Description = "Press calibration."
            };

            modelBuilder.Entity<Job>().HasData(new object[]
            {
                jobTurnOnLED,
                jobTurnOffLED,
                jobTurnOnWaterPump,
                jobMoveTeddyBear,
                jobFillCubeWithWater,
                jobRunAnySequence,
                jobRunAnyDifferenceSequence,
                jobSetInitialPose,
                jobMagicanPutTheSampleOnThePress,
                jobPressSqueezeTheSample,
                jobPressReleaseTheSample,
                jobPressCalibration
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
                DeviceTypeId = roboArmArexxDevType.Id,
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
                DeviceTypeId = roboArmDobotMagicianV2DevType.Id,
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
