using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SportComplexApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSportTrainerConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SportTrainers_Sports_SportId",
                table: "SportTrainers");

            migrationBuilder.DropForeignKey(
                name: "FK_SportTrainers_Trainers_TrainerId",
                table: "SportTrainers");

            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "Trainers");

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

            migrationBuilder.InsertData(
                table: "SportTrainers",
                columns: new[] { "SportId", "TrainerId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_SportTrainers_Sports_SportId",
                table: "SportTrainers",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SportTrainers_Trainers_TrainerId",
                table: "SportTrainers",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SportTrainers_Sports_SportId",
                table: "SportTrainers");

            migrationBuilder.DropForeignKey(
                name: "FK_SportTrainers_Trainers_TrainerId",
                table: "SportTrainers");

            migrationBuilder.DeleteData(
                table: "SportTrainers",
                keyColumns: new[] { "SportId", "TrainerId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "SportTrainers",
                keyColumns: new[] { "SportId", "TrainerId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                table: "Trainers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

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
                column: "Specialization",
                value: "Fitness");

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Specialization",
                value: "Yoga");

            migrationBuilder.AddForeignKey(
                name: "FK_SportTrainers_Sports_SportId",
                table: "SportTrainers",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SportTrainers_Trainers_TrainerId",
                table: "SportTrainers",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
