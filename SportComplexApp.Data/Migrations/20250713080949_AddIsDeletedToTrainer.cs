using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportComplexApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedToTrainer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Trainers",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsDeleted",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Trainers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bac8a10c-6068-4e21-9c23-118e0aa8c3bf", "bf387fd4-41ca-4097-9111-c92589889879" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a4e7b49d-7ddf-42d1-ba61-fd778f8e5932", "c5e79e59-3bb9-44f5-b3c2-2f1d407c3773" });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 7, 14, 10, 56, 47, 615, DateTimeKind.Local).AddTicks(6913));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 7, 14, 10, 56, 47, 616, DateTimeKind.Local).AddTicks(5366));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReservationDateTime",
                value: new DateTime(2025, 7, 15, 10, 56, 47, 616, DateTimeKind.Local).AddTicks(5424));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2025, 8, 13, 10, 56, 47, 618, DateTimeKind.Local).AddTicks(4865));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartDate",
                value: new DateTime(2025, 10, 13, 10, 56, 47, 618, DateTimeKind.Local).AddTicks(4923));

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 7, 13, 12, 56, 47, 619, DateTimeKind.Local).AddTicks(7793), new DateTime(2025, 7, 13, 11, 56, 47, 619, DateTimeKind.Local).AddTicks(7703) });

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 7, 13, 14, 56, 47, 619, DateTimeKind.Local).AddTicks(7798), new DateTime(2025, 7, 13, 13, 56, 47, 619, DateTimeKind.Local).AddTicks(7797) });
        }
    }
}
