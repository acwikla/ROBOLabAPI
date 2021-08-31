using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ROBOLab.API.Migrations
{
    public partial class AddColumnsToDevJobTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "DeviceJobs",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusChanged",
                table: "DeviceJobs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 8, 31, 2, 24, 43, 545, DateTimeKind.Local).AddTicks(9245));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 8, 31, 2, 24, 43, 548, DateTimeKind.Local).AddTicks(2916));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2021, 8, 31, 2, 24, 43, 548, DateTimeKind.Local).AddTicks(2953));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2021, 8, 31, 2, 24, 43, 548, DateTimeKind.Local).AddTicks(2958));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2021, 8, 31, 2, 24, 43, 548, DateTimeKind.Local).AddTicks(2961));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "DeviceJobs");

            migrationBuilder.DropColumn(
                name: "StatusChanged",
                table: "DeviceJobs");

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
        }
    }
}
