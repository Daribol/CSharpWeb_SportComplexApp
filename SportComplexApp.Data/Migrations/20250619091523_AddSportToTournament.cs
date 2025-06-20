using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportComplexApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSportToTournament : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SportId",
                table: "Tournaments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5eb3a124-bd43-4b54-ba6f-d64ab3f2a4f8", "50da9bc3-7ee3-4aff-aa58-5804074bce90" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1b4c61d1-623c-4d7d-a882-6edcdd42384c", "6366ffd3-9856-4660-ab96-ad7667629624" });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 6, 20, 12, 15, 23, 18, DateTimeKind.Local).AddTicks(127));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 6, 20, 12, 15, 23, 18, DateTimeKind.Local).AddTicks(3902));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReservationDateTime",
                value: new DateTime(2025, 6, 21, 12, 15, 23, 18, DateTimeKind.Local).AddTicks(3924));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "SportId", "StartDate" },
                values: new object[] { 1, new DateTime(2025, 7, 19, 12, 15, 23, 19, DateTimeKind.Local).AddTicks(5275) });

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "SportId", "StartDate" },
                values: new object[] { 2, new DateTime(2025, 9, 19, 12, 15, 23, 19, DateTimeKind.Local).AddTicks(5315) });

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 19, 14, 15, 23, 20, DateTimeKind.Local).AddTicks(1707), new DateTime(2025, 6, 19, 13, 15, 23, 20, DateTimeKind.Local).AddTicks(1683) });

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 19, 16, 15, 23, 20, DateTimeKind.Local).AddTicks(1712), new DateTime(2025, 6, 19, 15, 15, 23, 20, DateTimeKind.Local).AddTicks(1711) });

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_SportId",
                table: "Tournaments",
                column: "SportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Sports_SportId",
                table: "Tournaments",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Sports_SportId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_SportId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "SportId",
                table: "Tournaments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f7eb6d7d-2440-4f70-b396-18cd607adde6", "87236713-2c9f-48d5-a7a9-3bd523e270c0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0015ee43-1dbf-4849-9b51-f542e7dbdc4d", "af7ac024-e0f1-407b-b729-a0f007dc699c" });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 6, 20, 10, 4, 15, 638, DateTimeKind.Local).AddTicks(7979));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 6, 20, 10, 4, 15, 639, DateTimeKind.Local).AddTicks(5721));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReservationDateTime",
                value: new DateTime(2025, 6, 21, 10, 4, 15, 639, DateTimeKind.Local).AddTicks(6523));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2025, 7, 19, 10, 4, 15, 641, DateTimeKind.Local).AddTicks(5248));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartDate",
                value: new DateTime(2025, 9, 19, 10, 4, 15, 641, DateTimeKind.Local).AddTicks(5411));

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 19, 12, 4, 15, 643, DateTimeKind.Local).AddTicks(170), new DateTime(2025, 6, 19, 11, 4, 15, 643, DateTimeKind.Local).AddTicks(106) });

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 19, 14, 4, 15, 643, DateTimeKind.Local).AddTicks(177), new DateTime(2025, 6, 19, 13, 4, 15, 643, DateTimeKind.Local).AddTicks(175) });
        }
    }
}
