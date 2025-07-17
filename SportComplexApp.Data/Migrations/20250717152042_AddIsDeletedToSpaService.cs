using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportComplexApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedToSpaService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SpaServices",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
                table: "SpaServices",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "SpaServices",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsDeleted",
                value: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SpaServices");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "175186fb-26cc-4371-b8d9-7c59a4718683", "59697694-e94c-4e6b-ab70-0dc24d503c34" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c44f22d8-0e85-45ef-b2a5-be9d82ce6bde", "df28a0c2-9df4-4e0b-8d3a-4eb9345bc5f7" });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 7, 14, 11, 9, 48, 864, DateTimeKind.Local).AddTicks(1923));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 7, 14, 11, 9, 48, 864, DateTimeKind.Local).AddTicks(5612));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReservationDateTime",
                value: new DateTime(2025, 7, 15, 11, 9, 48, 864, DateTimeKind.Local).AddTicks(5636));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2025, 8, 13, 11, 9, 48, 866, DateTimeKind.Local).AddTicks(1806));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartDate",
                value: new DateTime(2025, 10, 13, 11, 9, 48, 866, DateTimeKind.Local).AddTicks(1860));

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 7, 13, 13, 9, 48, 866, DateTimeKind.Local).AddTicks(8499), new DateTime(2025, 7, 13, 12, 9, 48, 866, DateTimeKind.Local).AddTicks(8472) });

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 7, 13, 15, 9, 48, 866, DateTimeKind.Local).AddTicks(8504), new DateTime(2025, 7, 13, 14, 9, 48, 866, DateTimeKind.Local).AddTicks(8502) });
        }
    }
}
