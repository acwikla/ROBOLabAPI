﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ROBOLab.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Login = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Properties = table.Column<string>(type: "TEXT", nullable: true),
                    DeviceTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_DeviceTypes_DeviceTypeId",
                        column: x => x.DeviceTypeId,
                        principalTable: "DeviceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_DeviceTypes_DeviceTypeId",
                        column: x => x.DeviceTypeId,
                        principalTable: "DeviceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Devices_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceJobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExecutionTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Done = table.Column<bool>(type: "INTEGER", nullable: false),
                    Body = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceId = table.Column<int>(type: "INTEGER", nullable: false),
                    JobId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceJobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceJobs_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceJobs_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Modes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modes_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Body = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsMode = table.Column<bool>(type: "INTEGER", nullable: false),
                    ModeId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_DeviceTypes_DeviceTypeId",
                        column: x => x.DeviceTypeId,
                        principalTable: "DeviceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_Modes_ModeId",
                        column: x => x.ModeId,
                        principalTable: "Modes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Values",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Val = table.Column<string>(type: "TEXT", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PropertyId = table.Column<int>(type: "INTEGER", nullable: false),
                    DeviceId = table.Column<int>(type: "INTEGER", nullable: false),
                    DeviceJobId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Values_DeviceJobs_DeviceJobId",
                        column: x => x.DeviceJobId,
                        principalTable: "DeviceJobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Values_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Values_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DeviceTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "SmartTerra" });

            migrationBuilder.InsertData(
                table: "DeviceTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "RoboArm(Arexx RA-1-PRO)" });

            migrationBuilder.InsertData(
                table: "DeviceTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Dobot Magician V2" });

            migrationBuilder.InsertData(
                table: "DeviceTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "ROBOLab Press" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Login", "Password" },
                values: new object[] { 1, "buuu.email@gmail.com", "ola", "pass1" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Login", "Password" },
                values: new object[] { 2, "daniel.email@gmail.com", "daniel", "pass2" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Login", "Password" },
                values: new object[] { 3, "roboLab.email@gmail.com", "RoboLab User", "pass3" });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "DeviceTypeId", "Name", "UserId" },
                values: new object[] { 120, 4, "ROBOLab Press", 3 });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "DeviceTypeId", "Name", "UserId" },
                values: new object[] { 3, 1, "Test device 2 (SmartTerra)", 2 });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "DeviceTypeId", "Name", "UserId" },
                values: new object[] { 2, 2, "RoboArm1", 1 });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "DeviceTypeId", "Name", "UserId" },
                values: new object[] { 1, 1, "Test device 1 (SmartTerra)", 1 });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "DeviceTypeId", "Name", "UserId" },
                values: new object[] { 100, 3, "Dobot Magician V2 (RoboArm)", 3 });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Description", "DeviceTypeId", "Name", "Properties" },
                values: new object[] { 121, "Release the sample.", 4, "Release The Sample", "" });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Description", "DeviceTypeId", "Name", "Properties" },
                values: new object[] { 120, "Squeeze the sample.", 4, "Squeeze The Sample", "" });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Description", "DeviceTypeId", "Name", "Properties" },
                values: new object[] { 100, "Put the sample on the press.", 3, "Put the sample on the press", "" });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Description", "DeviceTypeId", "Name", "Properties" },
                values: new object[] { 1, "Turn on the LED strip and set color of the LEDs .", 1, "TurnOnLED", "" });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Description", "DeviceTypeId", "Name", "Properties" },
                values: new object[] { 6, "Run the provided sequence of angles.", 2, "RunAnySequence", "" });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Description", "DeviceTypeId", "Name", "Properties" },
                values: new object[] { 5, "Pour water into the cube for given period of time.", 2, "FillCubeWithWater", "" });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Description", "DeviceTypeId", "Name", "Properties" },
                values: new object[] { 4, "Move the teddy bear to a specific place.", 2, "MoveTeddyBear", "" });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Description", "DeviceTypeId", "Name", "Properties" },
                values: new object[] { 3, "Turn on the water pump for given period of time.", 1, "TurnOnWaterPump", "" });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Description", "DeviceTypeId", "Name", "Properties" },
                values: new object[] { 2, "Turn off the LED strip.", 1, "TurnOffLED", "" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Body", "DeviceTypeId", "IsMode", "ModeId", "Name" },
                values: new object[] { 4, "type: int, min: 0, max: 180", 2, false, null, "Angle Of Fourth Channel" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Body", "DeviceTypeId", "IsMode", "ModeId", "Name" },
                values: new object[] { 2, "type: int, min: 0, max: 180", 2, false, null, "Angle Of Second Channel" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Body", "DeviceTypeId", "IsMode", "ModeId", "Name" },
                values: new object[] { 6, "type: int, min: 0, max: 180", 2, false, null, "Angle Of Sixth Channel" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Body", "DeviceTypeId", "IsMode", "ModeId", "Name" },
                values: new object[] { 1, "type: int, min: 0, max: 180", 2, false, null, "Angle Of First Channel" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Body", "DeviceTypeId", "IsMode", "ModeId", "Name" },
                values: new object[] { 100, "type: float, min: null, max: null", 3, false, null, "Angle" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Body", "DeviceTypeId", "IsMode", "ModeId", "Name" },
                values: new object[] { 120, "type: float, min: 0, max: 10000000", 4, false, null, "Pressure force" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Body", "DeviceTypeId", "IsMode", "ModeId", "Name" },
                values: new object[] { 3, "type: int, min: 0, max: 180", 2, false, null, "Angle Of Third Channel" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Body", "DeviceTypeId", "IsMode", "ModeId", "Name" },
                values: new object[] { 5, "type: int, min: 0, max: 180", 2, false, null, "Angle Of Fifth Channel" });

            migrationBuilder.InsertData(
                table: "DeviceJobs",
                columns: new[] { "Id", "Body", "CreatedDate", "DeviceId", "Done", "ExecutionTime", "JobId" },
                values: new object[] { 1, "#FF6611", new DateTime(2021, 8, 2, 22, 59, 10, 67, DateTimeKind.Local).AddTicks(6617), 1, false, null, 1 });

            migrationBuilder.InsertData(
                table: "DeviceJobs",
                columns: new[] { "Id", "Body", "CreatedDate", "DeviceId", "Done", "ExecutionTime", "JobId" },
                values: new object[] { 2, "", new DateTime(2021, 8, 2, 22, 59, 10, 70, DateTimeKind.Local).AddTicks(881), 1, false, null, 2 });

            migrationBuilder.InsertData(
                table: "DeviceJobs",
                columns: new[] { "Id", "Body", "CreatedDate", "DeviceId", "Done", "ExecutionTime", "JobId" },
                values: new object[] { 3, "", new DateTime(2021, 8, 2, 22, 59, 10, 70, DateTimeKind.Local).AddTicks(921), 100, false, null, 100 });

            migrationBuilder.InsertData(
                table: "DeviceJobs",
                columns: new[] { "Id", "Body", "CreatedDate", "DeviceId", "Done", "ExecutionTime", "JobId" },
                values: new object[] { 4, "", new DateTime(2021, 8, 2, 22, 59, 10, 70, DateTimeKind.Local).AddTicks(927), 120, false, null, 120 });

            migrationBuilder.InsertData(
                table: "DeviceJobs",
                columns: new[] { "Id", "Body", "CreatedDate", "DeviceId", "Done", "ExecutionTime", "JobId" },
                values: new object[] { 5, "", new DateTime(2021, 8, 2, 22, 59, 10, 70, DateTimeKind.Local).AddTicks(930), 120, false, null, 121 });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceJobs_DeviceId",
                table: "DeviceJobs",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceJobs_JobId",
                table: "DeviceJobs",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeviceTypeId",
                table: "Devices",
                column: "DeviceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_UserId",
                table: "Devices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_DeviceTypeId",
                table: "Jobs",
                column: "DeviceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Modes_DeviceId",
                table: "Modes",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_DeviceTypeId",
                table: "Properties",
                column: "DeviceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_ModeId",
                table: "Properties",
                column: "ModeId");

            migrationBuilder.CreateIndex(
                name: "IX_Values_DeviceId",
                table: "Values",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Values_DeviceJobId",
                table: "Values",
                column: "DeviceJobId");

            migrationBuilder.CreateIndex(
                name: "IX_Values_PropertyId",
                table: "Values",
                column: "PropertyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Values");

            migrationBuilder.DropTable(
                name: "DeviceJobs");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Modes");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "DeviceTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
