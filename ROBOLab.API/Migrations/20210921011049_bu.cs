using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ROBOLab.API.Migrations
{
    public partial class bu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 9, 21, 3, 10, 48, 662, DateTimeKind.Local).AddTicks(5418));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 9, 21, 3, 10, 48, 665, DateTimeKind.Local).AddTicks(6161));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2021, 9, 21, 3, 10, 48, 665, DateTimeKind.Local).AddTicks(6204));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2021, 9, 21, 3, 10, 48, 665, DateTimeKind.Local).AddTicks(6210));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2021, 9, 21, 3, 10, 48, 665, DateTimeKind.Local).AddTicks(6217));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 9, 21, 3, 0, 56, 62, DateTimeKind.Local).AddTicks(1882));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 9, 21, 3, 0, 56, 69, DateTimeKind.Local).AddTicks(1637));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2021, 9, 21, 3, 0, 56, 69, DateTimeKind.Local).AddTicks(1730));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2021, 9, 21, 3, 0, 56, 69, DateTimeKind.Local).AddTicks(1744));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2021, 9, 21, 3, 0, 56, 69, DateTimeKind.Local).AddTicks(1752));
        }
    }
}
