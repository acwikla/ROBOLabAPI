using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ROBOLab.API.Migrations
{
    public partial class AddNewPressAndDobotData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 8, 9, 4, 5, 15, 153, DateTimeKind.Local).AddTicks(2401));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 8, 9, 4, 5, 15, 156, DateTimeKind.Local).AddTicks(351));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2021, 8, 9, 4, 5, 15, 156, DateTimeKind.Local).AddTicks(383));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2021, 8, 9, 4, 5, 15, 156, DateTimeKind.Local).AddTicks(389));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2021, 8, 9, 4, 5, 15, 156, DateTimeKind.Local).AddTicks(392));

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 100,
                column: "Properties",
                value: "X, type: float; Y, type: float; Z, type: float");

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Description", "DeviceTypeId", "Name", "Properties" },
                values: new object[] { 122, "Press calibration.", 4, "Press calibration", "stampHeight, type: float, min: 0, max: 6" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 120,
                column: "Body",
                value: "type: float, min: 0, max: 200");

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Body", "DeviceTypeId", "IsMode", "ModeId", "Name" },
                values: new object[] { 121, "type: float", 3, false, null, "X" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Body", "DeviceTypeId", "IsMode", "ModeId", "Name" },
                values: new object[] { 122, "type: float", 3, false, null, "Y" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Body", "DeviceTypeId", "IsMode", "ModeId", "Name" },
                values: new object[] { 123, "type: float", 3, false, null, "Z" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Body", "DeviceTypeId", "IsMode", "ModeId", "Name" },
                values: new object[] { 124, "type: float", 3, false, null, "R" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Body", "DeviceTypeId", "IsMode", "ModeId", "Name" },
                values: new object[] { 125, "type: float", 3, false, null, "First Angle" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Body", "DeviceTypeId", "IsMode", "ModeId", "Name" },
                values: new object[] { 126, "type: float", 3, false, null, "Second Angle" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Body", "DeviceTypeId", "IsMode", "ModeId", "Name" },
                values: new object[] { 127, "type: float", 3, false, null, "Third Angle" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 8, 2, 22, 59, 10, 67, DateTimeKind.Local).AddTicks(6617));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 8, 2, 22, 59, 10, 70, DateTimeKind.Local).AddTicks(881));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2021, 8, 2, 22, 59, 10, 70, DateTimeKind.Local).AddTicks(921));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2021, 8, 2, 22, 59, 10, 70, DateTimeKind.Local).AddTicks(927));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2021, 8, 2, 22, 59, 10, 70, DateTimeKind.Local).AddTicks(930));

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 100,
                column: "Properties",
                value: "");

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 120,
                column: "Body",
                value: "type: float, min: 0, max: 10000000");
        }
    }
}
