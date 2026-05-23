using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportComplexApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddReservationView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        CREATE VIEW [22180008].[v_ReservationRevenues]
        AS
        SELECT 
            r.Id AS ReservationId,
            u.FirstName + ' ' + u.LastName AS ClientName,
            s.Name AS SportName,
            r.NumberOfPeople,
            s.Price AS PricePerPerson,
            (r.NumberOfPeople * s.Price) AS ComputedTotalRevenue 
        FROM [22180008].[Reservations] r
        INNER JOIN [22180008].[Sports] s ON r.SportId = s.Id
        INNER JOIN [22180008].[AspNetUsers] u ON r.ClientId = u.Id;
    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW [22180008].[v_ReservationRevenues]");
        }
    }
}
