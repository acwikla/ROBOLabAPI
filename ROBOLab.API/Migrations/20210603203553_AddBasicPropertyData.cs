using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ROBOLab.API.Migrations
{
    public partial class AddBasicPropertyData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 6, 3, 22, 35, 53, 25, DateTimeKind.Local).AddTicks(7045));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 6, 3, 22, 35, 53, 28, DateTimeKind.Local).AddTicks(5592));

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Body", "DeviceTypeId", "IsMode", "ModeId", "Name" },
                values: new object[] { 2, "type: int, min: 0, max: 180", 2, false, null, "Angle Of Second Channel" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Body", "DeviceTypeId", "IsMode", "ModeId", "Name" },
                values: new object[] { 3, "type: int, min: 0, max: 180", 2, false, null, "Angle Of Third Channel" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Body", "DeviceTypeId", "IsMode", "ModeId", "Name" },
                values: new object[] { 4, "type: int, min: 0, max: 180", 2, false, null, "Angle Of Fourth Channel" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Body", "DeviceTypeId", "IsMode", "ModeId", "Name" },
                values: new object[] { 5, "type: int, min: 0, max: 180", 2, false, null, "Angle Of Fifth Channel" });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Body", "DeviceTypeId", "IsMode", "ModeId", "Name" },
                values: new object[] { 6, "type: int, min: 0, max: 180", 2, false, null, "Angle Of Sixth Channel" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 6, 3, 22, 29, 17, 984, DateTimeKind.Local).AddTicks(661));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 6, 3, 22, 29, 17, 986, DateTimeKind.Local).AddTicks(6885));
        }
    }
}
