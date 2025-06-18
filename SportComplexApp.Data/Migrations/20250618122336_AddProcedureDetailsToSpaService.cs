using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportComplexApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProcedureDetailsToSpaService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProcedureDetails",
                table: "SpaServices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
                column: "ProcedureDetails",
                value: "This massage focuses on relaxation and stress relief, using gentle techniques to soothe the body and mind.");

            migrationBuilder.UpdateData(
                table: "SpaServices",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProcedureDetails",
                value: "This facial treatment includes cleansing, exfoliation, and moisturizing to improve skin texture and appearance.");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcedureDetails",
                table: "SpaServices");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2c282bf2-2e4a-44a8-84f1-660109bcabc9", "aef1d6b0-524f-4fb4-9448-6eff154760de" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8c484d24-b70a-452f-8e49-bcdff3a791c4", "00046ddc-0d30-4188-b388-45c7c45bad0f" });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 6, 17, 13, 6, 21, 942, DateTimeKind.Local).AddTicks(1359));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 6, 17, 13, 6, 21, 942, DateTimeKind.Local).AddTicks(5095));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReservationDateTime",
                value: new DateTime(2025, 6, 18, 13, 6, 21, 942, DateTimeKind.Local).AddTicks(5141));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2025, 7, 16, 13, 6, 21, 943, DateTimeKind.Local).AddTicks(9797));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartDate",
                value: new DateTime(2025, 9, 16, 13, 6, 21, 943, DateTimeKind.Local).AddTicks(9848));

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 16, 15, 6, 21, 944, DateTimeKind.Local).AddTicks(8204), new DateTime(2025, 6, 16, 14, 6, 21, 944, DateTimeKind.Local).AddTicks(8159) });

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 16, 17, 6, 21, 944, DateTimeKind.Local).AddTicks(8210), new DateTime(2025, 6, 16, 16, 6, 21, 944, DateTimeKind.Local).AddTicks(8208) });
        }
    }
}
