using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ROBOLab.API.Migrations
{
    public partial class AddNewBasicData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 6, 3, 20, 2, 35, 886, DateTimeKind.Local).AddTicks(5027));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 6, 3, 20, 2, 35, 890, DateTimeKind.Local).AddTicks(8951));

            migrationBuilder.UpdateData(
                table: "DeviceTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "RoboArm(Arexx RA-1-PRO)");

            migrationBuilder.UpdateData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DeviceTypeId", "Name", "UserId" },
                values: new object[] { 2, "RoboArm1", 1 });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "DeviceTypeId", "Name", "UserId" },
                values: new object[] { 3, 1, "Test device 2 (SmartTerra)", 2 });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Description", "DeviceTypeId", "Name", "Properties" },
                values: new object[] { 4, "Move the teddy bear to a specific place.", 2, "MoveTeddyBear", "" });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Description", "DeviceTypeId", "Name", "Properties" },
                values: new object[] { 5, "Pour water into the cube for given period of time.", 2, "FillCubeWithWater", "" });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Description", "DeviceTypeId", "Name", "Properties" },
                values: new object[] { 6, "Run the provided sequence of angles.", 2, "RunAnySequence", "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 6, 2, 19, 2, 57, 988, DateTimeKind.Local).AddTicks(651));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 6, 2, 19, 2, 57, 992, DateTimeKind.Local).AddTicks(367));

            migrationBuilder.UpdateData(
                table: "DeviceTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Device type test");

            migrationBuilder.UpdateData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DeviceTypeId", "Name", "UserId" },
                values: new object[] { 1, "Test device 2 (SmartTerra)", 2 });
        }
    }
}
