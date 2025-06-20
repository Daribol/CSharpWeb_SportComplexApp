using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportComplexApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeededImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                table: "SpaServices",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "/images/RelaxingMassage.jpg");

            migrationBuilder.UpdateData(
                table: "SpaServices",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "/images/FacialTreatment.jpg");

            migrationBuilder.UpdateData(
                table: "Sports",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FacilityId", "ImageUrl" },
                values: new object[] { 3, "/images/Tennis.jpg" });

            migrationBuilder.UpdateData(
                table: "Sports",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "/images/swimming.jpg");

            migrationBuilder.InsertData(
                table: "Sports",
                columns: new[] { "Id", "Duration", "FacilityId", "ImageUrl", "Name", "Price" },
                values: new object[] { 3, 45, 1, "/images/football.jpg", "Football", 10.00m });

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

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "/images/JohnDoe.jpg");

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "/images/JaneSmith.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sports",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3fb0ae5d-0388-4d3f-8020-33d3f3f2b715", "316fbc91-ff29-4bac-84a0-fd33860a4cb2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "103e8368-ca2c-4088-a0fe-ce563c884b3b", "d050b79d-39a8-4ed9-85d2-3da1c164f306" });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 6, 19, 15, 23, 35, 460, DateTimeKind.Local).AddTicks(9748));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 6, 19, 15, 23, 35, 461, DateTimeKind.Local).AddTicks(6530));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReservationDateTime",
                value: new DateTime(2025, 6, 20, 15, 23, 35, 461, DateTimeKind.Local).AddTicks(6579));

            migrationBuilder.UpdateData(
                table: "SpaServices",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://example.com/images/relaxing-massage.jpg");

            migrationBuilder.UpdateData(
                table: "SpaServices",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://example.com/images/facial-treatment.jpg");

            migrationBuilder.UpdateData(
                table: "Sports",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FacilityId", "ImageUrl" },
                values: new object[] { 1, "https://example.com/tennis.jpg" });

            migrationBuilder.UpdateData(
                table: "Sports",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://example.com/swimming.jpg");

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2025, 7, 18, 15, 23, 35, 463, DateTimeKind.Local).AddTicks(4207));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartDate",
                value: new DateTime(2025, 9, 18, 15, 23, 35, 463, DateTimeKind.Local).AddTicks(4287));

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 18, 17, 23, 35, 464, DateTimeKind.Local).AddTicks(3756), new DateTime(2025, 6, 18, 16, 23, 35, 464, DateTimeKind.Local).AddTicks(3701) });

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 18, 19, 23, 35, 464, DateTimeKind.Local).AddTicks(3761), new DateTime(2025, 6, 18, 18, 23, 35, 464, DateTimeKind.Local).AddTicks(3759) });

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://example.com/images/john_doe.jpg");

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://example.com/images/jane_smith.jpg");
        }
    }
}
