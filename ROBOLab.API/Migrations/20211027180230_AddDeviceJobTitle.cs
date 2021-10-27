using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ROBOLab.API.Migrations
{
    public partial class AddDeviceJobTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "DeviceJobs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Title" },
                values: new object[] { new DateTime(2021, 10, 27, 20, 2, 30, 180, DateTimeKind.Local).AddTicks(4826), "Turn On LED1" });

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Title" },
                values: new object[] { new DateTime(2021, 10, 27, 20, 2, 30, 183, DateTimeKind.Local).AddTicks(235), "Turn off LED1" });

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Title" },
                values: new object[] { new DateTime(2021, 10, 27, 20, 2, 30, 183, DateTimeKind.Local).AddTicks(264), "Magician arm v2: put the sample on the press" });

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "Title" },
                values: new object[] { new DateTime(2021, 10, 27, 20, 2, 30, 183, DateTimeKind.Local).AddTicks(269), "Robo lab press: squeeze the sample" });

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "Title" },
                values: new object[] { new DateTime(2021, 10, 27, 20, 2, 30, 183, DateTimeKind.Local).AddTicks(272), "Robo lab press: release the sample" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "DeviceJobs");

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
    }
}
