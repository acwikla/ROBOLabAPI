﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ROBOLab.API.Migrations
{
    public partial class UpdateAutogeneratedDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Values",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 6, 4, 0, 23, 22, 845, DateTimeKind.Local).AddTicks(7559));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 6, 4, 0, 23, 22, 849, DateTimeKind.Local).AddTicks(8006));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Values",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 6, 4, 0, 20, 7, 294, DateTimeKind.Local).AddTicks(5035));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 6, 4, 0, 20, 7, 298, DateTimeKind.Local).AddTicks(6167));
        }
    }
}
