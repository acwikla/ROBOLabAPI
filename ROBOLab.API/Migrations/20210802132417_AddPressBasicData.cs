using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ROBOLab.API.Migrations
{
    public partial class AddPressBasicData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 8, 2, 15, 24, 17, 38, DateTimeKind.Local).AddTicks(282));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 8, 2, 15, 24, 17, 40, DateTimeKind.Local).AddTicks(7517));

            migrationBuilder.InsertData(
                table: "DeviceTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Press" });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Description", "DeviceTypeId", "Name", "Properties" },
                values: new object[] { 7, "Put the sample on the press.", 3, "Move The Sample", "" });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Description", "DeviceTypeId", "Name", "Properties" },
                values: new object[] { 8, "Squeeze the sample.", 3, "Squeeze The Sample", "" });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Description", "DeviceTypeId", "Name", "Properties" },
                values: new object[] { 9, "Release the sample.", 3, "Release The Sample", "" });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "DeviceTypeId", "Name", "UserId" },
                values: new object[] { 5, 4, "Press", 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "DeviceTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 7, 29, 0, 6, 40, 169, DateTimeKind.Local).AddTicks(3428));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 7, 29, 0, 6, 40, 173, DateTimeKind.Local).AddTicks(3460));
        }
    }
}
