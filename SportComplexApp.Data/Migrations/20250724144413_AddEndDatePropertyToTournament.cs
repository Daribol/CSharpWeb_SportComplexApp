using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportComplexApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEndDatePropertyToTournament : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Tournaments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5a2b9189-00eb-404c-b8c8-b3e13682d4aa", "d522028f-2e35-4ce6-a4ab-20da0a90ebc2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a0309e20-f7db-42e2-9165-1db96b915ea6", "c7eb2968-91ae-40bd-b71c-58fd922a2ed5" });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 7, 25, 17, 44, 12, 452, DateTimeKind.Local).AddTicks(9826));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 7, 25, 17, 44, 12, 453, DateTimeKind.Local).AddTicks(4103));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReservationDateTime",
                value: new DateTime(2025, 7, 26, 17, 44, 12, 453, DateTimeKind.Local).AddTicks(4148));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 24, 17, 44, 12, 454, DateTimeKind.Local).AddTicks(8503) });

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 24, 17, 44, 12, 454, DateTimeKind.Local).AddTicks(8560) });

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 7, 24, 19, 44, 12, 455, DateTimeKind.Local).AddTicks(5512), new DateTime(2025, 7, 24, 18, 44, 12, 455, DateTimeKind.Local).AddTicks(5471) });

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 7, 24, 21, 44, 12, 455, DateTimeKind.Local).AddTicks(5517), new DateTime(2025, 7, 24, 20, 44, 12, 455, DateTimeKind.Local).AddTicks(5516) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Tournaments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c0d6eda7-6124-47de-8106-9eaed74aadac", "2dddc142-1bf9-4b15-b44b-2422f20ef3d0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a0456c4c-3d64-4999-a5e3-6334b698571c", "bc1f1c06-ec48-466b-a11f-8ed6c8e660a2" });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 7, 22, 23, 54, 25, 475, DateTimeKind.Local).AddTicks(9306));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 7, 22, 23, 54, 25, 476, DateTimeKind.Local).AddTicks(3099));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReservationDateTime",
                value: new DateTime(2025, 7, 23, 23, 54, 25, 476, DateTimeKind.Local).AddTicks(3137));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2025, 8, 21, 23, 54, 25, 477, DateTimeKind.Local).AddTicks(6355));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartDate",
                value: new DateTime(2025, 10, 21, 23, 54, 25, 477, DateTimeKind.Local).AddTicks(6427));

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 7, 22, 1, 54, 25, 478, DateTimeKind.Local).AddTicks(6001), new DateTime(2025, 7, 22, 0, 54, 25, 478, DateTimeKind.Local).AddTicks(5953) });

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 7, 22, 3, 54, 25, 478, DateTimeKind.Local).AddTicks(6006), new DateTime(2025, 7, 22, 2, 54, 25, 478, DateTimeKind.Local).AddTicks(6004) });
        }
    }
}
