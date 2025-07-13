using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportComplexApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedToSport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Sports",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
                table: "Sports",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Sports",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Sports",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsDeleted",
                value: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Sports");

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
    }
}
