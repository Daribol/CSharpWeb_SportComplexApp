using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportComplexApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDurationToSpaService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "SpaServices",
                type: "int",
                nullable: false,
                defaultValue: 60);

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
                table: "SpaServices",
                keyColumn: "Id",
                keyValue: 1,
                column: "Duration",
                value: 60);

            migrationBuilder.UpdateData(
                table: "SpaServices",
                keyColumn: "Id",
                keyValue: 2,
                column: "Duration",
                value: 90);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "SpaServices");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "66deb6cc-12d2-4dd2-9b15-800ace986098", "463d801c-f982-4ee4-b8ed-b699bc523900" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "cf93e1a2-ffd8-4bad-b4e1-efcbc0a2c506", "37e34c25-21b0-490a-a6db-7bb98e3fc369" });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 7, 19, 17, 24, 25, 335, DateTimeKind.Local).AddTicks(8530));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 7, 19, 17, 24, 25, 336, DateTimeKind.Local).AddTicks(2218));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReservationDateTime",
                value: new DateTime(2025, 7, 20, 17, 24, 25, 336, DateTimeKind.Local).AddTicks(2243));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2025, 8, 18, 17, 24, 25, 337, DateTimeKind.Local).AddTicks(4732));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartDate",
                value: new DateTime(2025, 10, 18, 17, 24, 25, 337, DateTimeKind.Local).AddTicks(4927));

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 7, 18, 19, 24, 25, 338, DateTimeKind.Local).AddTicks(6412), new DateTime(2025, 7, 18, 18, 24, 25, 338, DateTimeKind.Local).AddTicks(6362) });

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 7, 18, 21, 24, 25, 338, DateTimeKind.Local).AddTicks(6417), new DateTime(2025, 7, 18, 20, 24, 25, 338, DateTimeKind.Local).AddTicks(6416) });
        }
    }
}
