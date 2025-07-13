using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportComplexApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedToFacility : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Facilities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0265bd64-a777-4681-80bc-3f6e7dc6978a", "442b1d93-8290-4452-a8aa-c3b3b566f8a9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "71b1aa1d-4028-4af6-a98f-dc617695885d", "4a5e8c19-f591-4481-a568-c9c599b0e8f4" });

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 7, 14, 10, 34, 43, 214, DateTimeKind.Local).AddTicks(702));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 7, 14, 10, 34, 43, 214, DateTimeKind.Local).AddTicks(5033));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReservationDateTime",
                value: new DateTime(2025, 7, 15, 10, 34, 43, 214, DateTimeKind.Local).AddTicks(5084));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2025, 8, 13, 10, 34, 43, 216, DateTimeKind.Local).AddTicks(9));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartDate",
                value: new DateTime(2025, 10, 13, 10, 34, 43, 216, DateTimeKind.Local).AddTicks(72));

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 7, 13, 12, 34, 43, 216, DateTimeKind.Local).AddTicks(8309), new DateTime(2025, 7, 13, 11, 34, 43, 216, DateTimeKind.Local).AddTicks(8260) });

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 7, 13, 14, 34, 43, 216, DateTimeKind.Local).AddTicks(8314), new DateTime(2025, 7, 13, 13, 34, 43, 216, DateTimeKind.Local).AddTicks(8313) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Facilities");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c17d6bcf-84a8-4564-90b3-f94f68552146", "17877aa5-745d-45e2-a752-87a1aece31c8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "860cbfc7-4999-49ae-b2b8-bf3898362e3d", "a9f90e11-1de0-4b3d-9f5a-bfd35a7dea3c" });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 6, 25, 20, 56, 11, 336, DateTimeKind.Local).AddTicks(5838));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 6, 25, 20, 56, 11, 336, DateTimeKind.Local).AddTicks(9889));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReservationDateTime",
                value: new DateTime(2025, 6, 26, 20, 56, 11, 336, DateTimeKind.Local).AddTicks(9914));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2025, 7, 24, 20, 56, 11, 338, DateTimeKind.Local).AddTicks(1767));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartDate",
                value: new DateTime(2025, 9, 24, 20, 56, 11, 338, DateTimeKind.Local).AddTicks(1809));

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 24, 22, 56, 11, 339, DateTimeKind.Local).AddTicks(196), new DateTime(2025, 6, 24, 21, 56, 11, 339, DateTimeKind.Local).AddTicks(149) });

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 25, 0, 56, 11, 339, DateTimeKind.Local).AddTicks(204), new DateTime(2025, 6, 24, 23, 56, 11, 339, DateTimeKind.Local).AddTicks(202) });
        }
    }
}
