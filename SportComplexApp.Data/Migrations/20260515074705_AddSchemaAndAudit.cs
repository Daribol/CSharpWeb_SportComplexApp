using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportComplexApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSchemaAndAudit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "22180008");

            migrationBuilder.RenameTable(
                name: "TrainerSessions",
                newName: "TrainerSessions",
                newSchema: "22180008");

            migrationBuilder.RenameTable(
                name: "Trainers",
                newName: "Trainers",
                newSchema: "22180008");

            migrationBuilder.RenameTable(
                name: "Tournaments",
                newName: "Tournaments",
                newSchema: "22180008");

            migrationBuilder.RenameTable(
                name: "TournamentRegistrations",
                newName: "TournamentRegistrations",
                newSchema: "22180008");

            migrationBuilder.RenameTable(
                name: "SportTrainers",
                newName: "SportTrainers",
                newSchema: "22180008");

            migrationBuilder.RenameTable(
                name: "Sports",
                newName: "Sports",
                newSchema: "22180008");

            migrationBuilder.RenameTable(
                name: "SpaServices",
                newName: "SpaServices",
                newSchema: "22180008");

            migrationBuilder.RenameTable(
                name: "SpaReservations",
                newName: "SpaReservations",
                newSchema: "22180008");

            migrationBuilder.RenameTable(
                name: "Reservations",
                newName: "Reservations",
                newSchema: "22180008");

            migrationBuilder.RenameTable(
                name: "Facilities",
                newName: "Facilities",
                newSchema: "22180008");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "AspNetUserTokens",
                newSchema: "22180008");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "AspNetUsers",
                newSchema: "22180008");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "AspNetUserRoles",
                newSchema: "22180008");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "AspNetUserLogins",
                newSchema: "22180008");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "AspNetUserClaims",
                newSchema: "22180008");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "AspNetRoles",
                newSchema: "22180008");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "AspNetRoleClaims",
                newSchema: "22180008");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified_22180008",
                schema: "22180008",
                table: "TrainerSessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified_22180008",
                schema: "22180008",
                table: "Trainers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified_22180008",
                schema: "22180008",
                table: "Tournaments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified_22180008",
                schema: "22180008",
                table: "TournamentRegistrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified_22180008",
                schema: "22180008",
                table: "SportTrainers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified_22180008",
                schema: "22180008",
                table: "Sports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified_22180008",
                schema: "22180008",
                table: "SpaServices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified_22180008",
                schema: "22180008",
                table: "SpaReservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified_22180008",
                schema: "22180008",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified_22180008",
                schema: "22180008",
                table: "Facilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified_22180008",
                schema: "22180008",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Logs_22180008",
                schema: "22180008",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs_22180008", x => x.Id);
                });

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastModified_22180008",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastModified_22180008",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastModified_22180008",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 4,
                column: "LastModified_22180008",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "SpaServices",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastModified_22180008",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "SpaServices",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastModified_22180008",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "SpaServices",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastModified_22180008",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "SportTrainers",
                keyColumns: new[] { "SportId", "TrainerId" },
                keyValues: new object[] { 1, 1 },
                column: "LastModified_22180008",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "SportTrainers",
                keyColumns: new[] { "SportId", "TrainerId" },
                keyValues: new object[] { 2, 5 },
                column: "LastModified_22180008",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "SportTrainers",
                keyColumns: new[] { "SportId", "TrainerId" },
                keyValues: new object[] { 3, 4 },
                column: "LastModified_22180008",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "SportTrainers",
                keyColumns: new[] { "SportId", "TrainerId" },
                keyValues: new object[] { 4, 2 },
                column: "LastModified_22180008",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "SportTrainers",
                keyColumns: new[] { "SportId", "TrainerId" },
                keyValues: new object[] { 4, 3 },
                column: "LastModified_22180008",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "SportTrainers",
                keyColumns: new[] { "SportId", "TrainerId" },
                keyValues: new object[] { 5, 3 },
                column: "LastModified_22180008",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "Sports",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastModified_22180008",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "Sports",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastModified_22180008",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "Sports",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastModified_22180008",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "Sports",
                keyColumn: "Id",
                keyValue: 4,
                column: "LastModified_22180008",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "Sports",
                keyColumn: "Id",
                keyValue: 5,
                column: "LastModified_22180008",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "Sports",
                keyColumn: "Id",
                keyValue: 6,
                column: "LastModified_22180008",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastModified_22180008",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastModified_22180008",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastModified_22180008",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastModified_22180008",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastModified_22180008",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastModified_22180008",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 4,
                column: "LastModified_22180008",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 5,
                column: "LastModified_22180008",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs_22180008",
                schema: "22180008");

            migrationBuilder.DropColumn(
                name: "LastModified_22180008",
                schema: "22180008",
                table: "TrainerSessions");

            migrationBuilder.DropColumn(
                name: "LastModified_22180008",
                schema: "22180008",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "LastModified_22180008",
                schema: "22180008",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "LastModified_22180008",
                schema: "22180008",
                table: "TournamentRegistrations");

            migrationBuilder.DropColumn(
                name: "LastModified_22180008",
                schema: "22180008",
                table: "SportTrainers");

            migrationBuilder.DropColumn(
                name: "LastModified_22180008",
                schema: "22180008",
                table: "Sports");

            migrationBuilder.DropColumn(
                name: "LastModified_22180008",
                schema: "22180008",
                table: "SpaServices");

            migrationBuilder.DropColumn(
                name: "LastModified_22180008",
                schema: "22180008",
                table: "SpaReservations");

            migrationBuilder.DropColumn(
                name: "LastModified_22180008",
                schema: "22180008",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "LastModified_22180008",
                schema: "22180008",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "LastModified_22180008",
                schema: "22180008",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "TrainerSessions",
                schema: "22180008",
                newName: "TrainerSessions");

            migrationBuilder.RenameTable(
                name: "Trainers",
                schema: "22180008",
                newName: "Trainers");

            migrationBuilder.RenameTable(
                name: "Tournaments",
                schema: "22180008",
                newName: "Tournaments");

            migrationBuilder.RenameTable(
                name: "TournamentRegistrations",
                schema: "22180008",
                newName: "TournamentRegistrations");

            migrationBuilder.RenameTable(
                name: "SportTrainers",
                schema: "22180008",
                newName: "SportTrainers");

            migrationBuilder.RenameTable(
                name: "Sports",
                schema: "22180008",
                newName: "Sports");

            migrationBuilder.RenameTable(
                name: "SpaServices",
                schema: "22180008",
                newName: "SpaServices");

            migrationBuilder.RenameTable(
                name: "SpaReservations",
                schema: "22180008",
                newName: "SpaReservations");

            migrationBuilder.RenameTable(
                name: "Reservations",
                schema: "22180008",
                newName: "Reservations");

            migrationBuilder.RenameTable(
                name: "Facilities",
                schema: "22180008",
                newName: "Facilities");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "22180008",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                schema: "22180008",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "22180008",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                schema: "22180008",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                schema: "22180008",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                schema: "22180008",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                schema: "22180008",
                newName: "AspNetRoleClaims");
        }
    }
}
