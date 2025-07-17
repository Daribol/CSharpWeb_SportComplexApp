using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportComplexApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedToTournament : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Tournaments",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
                columns: new[] { "IsDeleted", "StartDate" },
                values: new object[] { false, new DateTime(2025, 8, 17, 21, 52, 43, 281, DateTimeKind.Local).AddTicks(2098) });

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsDeleted", "StartDate" },
                values: new object[] { false, new DateTime(2025, 10, 17, 21, 52, 43, 281, DateTimeKind.Local).AddTicks(2149) });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Tournaments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f3374f45-86b4-4ed2-b9f0-e26bd22223a4", "d7c19281-53cd-4d5d-b366-07f38bd78059" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9641d39f-7f12-4784-97ac-aeb8de59bd8b", "79de4dda-0111-4f23-bb24-de1651dc96b3" });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 7, 18, 18, 20, 41, 539, DateTimeKind.Local).AddTicks(9191));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 7, 18, 18, 20, 41, 540, DateTimeKind.Local).AddTicks(3635));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReservationDateTime",
                value: new DateTime(2025, 7, 19, 18, 20, 41, 540, DateTimeKind.Local).AddTicks(3672));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2025, 8, 17, 18, 20, 41, 541, DateTimeKind.Local).AddTicks(6665));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartDate",
                value: new DateTime(2025, 10, 17, 18, 20, 41, 541, DateTimeKind.Local).AddTicks(6717));

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 7, 17, 20, 20, 41, 542, DateTimeKind.Local).AddTicks(3995), new DateTime(2025, 7, 17, 19, 20, 41, 542, DateTimeKind.Local).AddTicks(3948) });

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 7, 17, 22, 20, 41, 542, DateTimeKind.Local).AddTicks(4001), new DateTime(2025, 7, 17, 21, 20, 41, 542, DateTimeKind.Local).AddTicks(4000) });
        }
    }
}
