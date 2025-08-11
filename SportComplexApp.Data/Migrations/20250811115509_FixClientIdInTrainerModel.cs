using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportComplexApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixClientIdInTrainerModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainers_AspNetUsers_ClientId",
                table: "Trainers");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "Trainers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7ff94d81-f45f-4901-9e07-d9b457d4694e", "dc67308f-ae97-4e27-b4e4-97f731c96d8a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9d1528ea-754c-45a9-83da-bd1fe4dd1fb5", "49d3e591-0044-4d10-934a-cf09c8f56d31" });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 8, 12, 14, 55, 8, 649, DateTimeKind.Local).AddTicks(9307));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 8, 12, 14, 55, 8, 650, DateTimeKind.Local).AddTicks(4566));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReservationDateTime",
                value: new DateTime(2025, 8, 13, 14, 55, 8, 650, DateTimeKind.Local).AddTicks(4679));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2025, 9, 11, 14, 55, 8, 652, DateTimeKind.Local).AddTicks(6879));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartDate",
                value: new DateTime(2025, 11, 11, 14, 55, 8, 652, DateTimeKind.Local).AddTicks(6954));

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 8, 11, 16, 55, 8, 653, DateTimeKind.Local).AddTicks(9908), new DateTime(2025, 8, 11, 15, 55, 8, 653, DateTimeKind.Local).AddTicks(9807) });

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 8, 11, 18, 55, 8, 653, DateTimeKind.Local).AddTicks(9915), new DateTime(2025, 8, 11, 17, 55, 8, 653, DateTimeKind.Local).AddTicks(9913) });

            migrationBuilder.AddForeignKey(
                name: "FK_Trainers_AspNetUsers_ClientId",
                table: "Trainers",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainers_AspNetUsers_ClientId",
                table: "Trainers");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "Trainers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6ae57139-bf7c-4890-b26a-4f9fce7e8d0a", "3545be0b-2098-4568-951a-bd3d206baac6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "18006d7e-7c53-4fc9-badd-1e6de5bc6cd9", "fbdc526a-9170-4eba-aa25-df7e5a82addf" });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 8, 2, 11, 7, 42, 59, DateTimeKind.Local).AddTicks(3007));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 8, 2, 11, 7, 42, 59, DateTimeKind.Local).AddTicks(7814));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReservationDateTime",
                value: new DateTime(2025, 8, 3, 11, 7, 42, 59, DateTimeKind.Local).AddTicks(7869));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2025, 9, 1, 11, 7, 42, 61, DateTimeKind.Local).AddTicks(4218));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartDate",
                value: new DateTime(2025, 11, 1, 11, 7, 42, 61, DateTimeKind.Local).AddTicks(4268));

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 8, 1, 13, 7, 42, 62, DateTimeKind.Local).AddTicks(1857), new DateTime(2025, 8, 1, 12, 7, 42, 62, DateTimeKind.Local).AddTicks(1822) });

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 8, 1, 15, 7, 42, 62, DateTimeKind.Local).AddTicks(1863), new DateTime(2025, 8, 1, 14, 7, 42, 62, DateTimeKind.Local).AddTicks(1861) });

            migrationBuilder.AddForeignKey(
                name: "FK_Trainers_AspNetUsers_ClientId",
                table: "Trainers",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
