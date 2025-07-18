using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportComplexApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMinAndMaxPeopleInSportModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxPeople",
                table: "Sports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinPeople",
                table: "Sports",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                table: "Sports",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MaxPeople", "MinPeople" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "Sports",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "MaxPeople", "MinPeople" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "Sports",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "MaxPeople", "MinPeople" },
                values: new object[] { 0, 0 });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxPeople",
                table: "Sports");

            migrationBuilder.DropColumn(
                name: "MinPeople",
                table: "Sports");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "94ece384-c3bf-4eba-b997-30e685ece14d", "f63a6030-e4e8-4ebf-858c-2d0200290072" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7ba8b01c-c1a3-4a20-906b-33d362ecb7dd", "c7a3657c-4644-4964-b9b2-5b8addc219de" });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 7, 18, 21, 52, 43, 278, DateTimeKind.Local).AddTicks(9844));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 7, 18, 21, 52, 43, 279, DateTimeKind.Local).AddTicks(4074));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReservationDateTime",
                value: new DateTime(2025, 7, 19, 21, 52, 43, 279, DateTimeKind.Local).AddTicks(4125));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2025, 8, 17, 21, 52, 43, 281, DateTimeKind.Local).AddTicks(2098));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartDate",
                value: new DateTime(2025, 10, 17, 21, 52, 43, 281, DateTimeKind.Local).AddTicks(2149));

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 7, 17, 23, 52, 43, 282, DateTimeKind.Local).AddTicks(475), new DateTime(2025, 7, 17, 22, 52, 43, 282, DateTimeKind.Local).AddTicks(421) });

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 7, 18, 1, 52, 43, 282, DateTimeKind.Local).AddTicks(482), new DateTime(2025, 7, 18, 0, 52, 43, 282, DateTimeKind.Local).AddTicks(480) });
        }
    }
}
