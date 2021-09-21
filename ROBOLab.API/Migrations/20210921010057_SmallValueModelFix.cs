using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ROBOLab.API.Migrations
{
    public partial class SmallValueModelFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 9, 16, 5, 6, 14, 888, DateTimeKind.Local).AddTicks(2604));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 9, 16, 5, 6, 14, 890, DateTimeKind.Local).AddTicks(5872));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2021, 9, 16, 5, 6, 14, 890, DateTimeKind.Local).AddTicks(5904));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2021, 9, 16, 5, 6, 14, 890, DateTimeKind.Local).AddTicks(5909));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2021, 9, 16, 5, 6, 14, 890, DateTimeKind.Local).AddTicks(5912));
        }
    }
}
