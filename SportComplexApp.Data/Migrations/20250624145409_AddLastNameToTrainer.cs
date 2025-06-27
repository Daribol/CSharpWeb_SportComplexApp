using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportComplexApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLastNameToTrainer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Trainers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "fca36b08-1603-4c0f-ba84-581cf9ea2ae1", "0e69f8c7-8f1f-4c87-9cd9-dcb8ce2b5dea" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "849e24f3-dff9-4ec5-9b3d-2ed718031879", "df077800-e08a-46bb-ae5f-8039d40adbd6" });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 6, 25, 17, 54, 8, 629, DateTimeKind.Local).AddTicks(9254));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservationDateTime",
                value: new DateTime(2025, 6, 25, 17, 54, 8, 630, DateTimeKind.Local).AddTicks(3364));

            migrationBuilder.UpdateData(
                table: "SpaReservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReservationDateTime",
                value: new DateTime(2025, 6, 26, 17, 54, 8, 630, DateTimeKind.Local).AddTicks(3391));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2025, 7, 24, 17, 54, 8, 631, DateTimeKind.Local).AddTicks(4459));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartDate",
                value: new DateTime(2025, 9, 24, 17, 54, 8, 631, DateTimeKind.Local).AddTicks(4493));

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 24, 19, 54, 8, 632, DateTimeKind.Local).AddTicks(995), new DateTime(2025, 6, 24, 18, 54, 8, 632, DateTimeKind.Local).AddTicks(974) });

            migrationBuilder.UpdateData(
                table: "TrainerSessions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 24, 21, 54, 8, 632, DateTimeKind.Local).AddTicks(1000), new DateTime(2025, 6, 24, 20, 54, 8, 632, DateTimeKind.Local).AddTicks(998) });

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LastName", "Name" },
                values: new object[] { "Doe", "John" });

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "LastName", "Name" },
                values: new object[] { "Smith", "Jane" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Trainers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
                column: "StartDate",
                value: new DateTime(2025, 7, 19, 12, 15, 23, 19, DateTimeKind.Local).AddTicks(5275));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartDate",
                value: new DateTime(2025, 9, 19, 12, 15, 23, 19, DateTimeKind.Local).AddTicks(5315));

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

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "John Doe");

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Jane Smith");
        }
    }
}
