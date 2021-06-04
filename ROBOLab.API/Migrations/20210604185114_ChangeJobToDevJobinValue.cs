using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ROBOLab.API.Migrations
{
    public partial class ChangeJobToDevJobinValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Values_Jobs_JobId",
                table: "Values");

            migrationBuilder.RenameColumn(
                name: "JobId",
                table: "Values",
                newName: "DeviceJobId");

            migrationBuilder.RenameIndex(
                name: "IX_Values_JobId",
                table: "Values",
                newName: "IX_Values_DeviceJobId");

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 6, 4, 20, 51, 13, 713, DateTimeKind.Local).AddTicks(4380));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 6, 4, 20, 51, 13, 715, DateTimeKind.Local).AddTicks(7606));

            migrationBuilder.AddForeignKey(
                name: "FK_Values_DeviceJobs_DeviceJobId",
                table: "Values",
                column: "DeviceJobId",
                principalTable: "DeviceJobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Values_DeviceJobs_DeviceJobId",
                table: "Values");

            migrationBuilder.RenameColumn(
                name: "DeviceJobId",
                table: "Values",
                newName: "JobId");

            migrationBuilder.RenameIndex(
                name: "IX_Values_DeviceJobId",
                table: "Values",
                newName: "IX_Values_JobId");

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2021, 6, 4, 0, 35, 13, 827, DateTimeKind.Local).AddTicks(2179));

            migrationBuilder.UpdateData(
                table: "DeviceJobs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2021, 6, 4, 0, 35, 13, 831, DateTimeKind.Local).AddTicks(895));

            migrationBuilder.AddForeignKey(
                name: "FK_Values_Jobs_JobId",
                table: "Values",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
