using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ROBOLab.API.Migrations
{
    public partial class AddModeDBInitData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                value: new DateTime(2021, 8, 10, 21, 36, 17, 939, DateTimeKind.Local).AddTicks(4642));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 8, 10, 21, 36, 17, 942, DateTimeKind.Local).AddTicks(6015));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2021, 8, 10, 21, 36, 17, 942, DateTimeKind.Local).AddTicks(6078));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2021, 8, 10, 21, 36, 17, 942, DateTimeKind.Local).AddTicks(6086));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2021, 8, 10, 21, 36, 17, 942, DateTimeKind.Local).AddTicks(6091));

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 100,
                column: "Properties",
                value: "name: X, type: float; name: Y, type: float; name: Z, type: float");

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 122,
                column: "Properties",
                value: "name: stampHeight, type: float, min: 0, max: 6");

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "Body", "Name" },
                values: new object[] { "type: float", "X" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Body", "DeviceTypeId", "IsMode", "ModeId", "Name" },
                values: new object[] { 101, "type: float", 3, false, null, "Y" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Body", "DeviceTypeId", "IsMode", "ModeId", "Name" },
                values: new object[] { 102, "type: float", 3, false, null, "Z" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Body", "DeviceTypeId", "IsMode", "ModeId", "Name" },
                values: new object[] { 103, "type: float", 3, false, null, "R" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Body", "DeviceTypeId", "IsMode", "ModeId", "Name" },
                values: new object[] { 104, "type: float", 3, false, null, "Angle 1" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Body", "DeviceTypeId", "IsMode", "ModeId", "Name" },
                values: new object[] { 105, "type: float", 3, false, null, "Angle 2" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Body", "DeviceTypeId", "IsMode", "ModeId", "Name" },
                values: new object[] { 106, "type: float", 3, false, null, "Angle 3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 106);

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

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 122,
                column: "Properties",
                value: "stampHeight, type: float, min: 0, max: 6");

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "Body", "Name" },
                values: new object[] { "type: float, min: null, max: null", "Angle" });

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
    }
}
