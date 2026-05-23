using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportComplexApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAuditTriggers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var singleKeyTables = new[]
            {
                "AspNetUsers", "Facilities", "Reservations",
                "SpaReservations", "SpaServices", "Sports",
                "TournamentRegistrations", "Tournaments",
                "Trainers", "TrainerSessions"
            };

            foreach (var table in singleKeyTables)
            {
                migrationBuilder.Sql($@"
                    ALTER TRIGGER [22180008].[trg_{table}_Audit]
                    ON [22180008].[{table}]
                    AFTER INSERT, UPDATE
                    AS
                    BEGIN
                        DECLARE @Operation VARCHAR(10);
                        
                        IF EXISTS(SELECT 1 FROM deleted) 
                            SET @Operation = 'UPDATE';
                        ELSE 
                            SET @Operation = 'INSERT';

                        IF EXISTS(SELECT 1 FROM inserted)
                        BEGIN
                            -- 1. Запис в журнала
                            INSERT INTO [22180008].[Logs_22180008] (TableName, OperationType, OperationDate)
                            VALUES ('{table}', @Operation, GETDATE());

                            -- 2. Обновяване на датата при редакция
                            IF @Operation = 'UPDATE'
                            BEGIN
                                UPDATE t
                                SET t.LastModified_22180008 = GETDATE()
                                FROM [22180008].[{table}] t
                                INNER JOIN inserted i ON t.Id = i.Id;
                            END
                        END
                    END
                ");
            }

            migrationBuilder.Sql(@"
                ALTER TRIGGER [22180008].[trg_SportTrainers_Audit]
                ON [22180008].[SportTrainers]
                AFTER INSERT, UPDATE
                AS
                BEGIN
                    DECLARE @Operation VARCHAR(10);
                    
                    IF EXISTS(SELECT 1 FROM deleted) 
                        SET @Operation = 'UPDATE';
                    ELSE 
                        SET @Operation = 'INSERT';

                    IF EXISTS(SELECT 1 FROM inserted)
                    BEGIN
                        INSERT INTO [22180008].[Logs_22180008] (TableName, OperationType, OperationDate)
                        VALUES ('SportTrainers', @Operation, GETDATE());

                        IF @Operation = 'UPDATE'
                        BEGIN
                            UPDATE t
                            SET t.LastModified_22180008 = GETDATE()
                            FROM [22180008].[SportTrainers] t
                            INNER JOIN inserted i ON t.SportId = i.SportId AND t.TrainerId = i.TrainerId;
                        END
                    END
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var allTables = new[]
            {
                "AspNetUsers", "Facilities", "Reservations",
                "SpaReservations", "SpaServices", "Sports",
                "SportTrainers", "TournamentRegistrations", "Tournaments",
                "Trainers", "TrainerSessions"
            };

            foreach (var table in allTables)
            {
                migrationBuilder.Sql($@"
                    ALTER TRIGGER [22180008].[trg_{table}_Audit]
                    ON [22180008].[{table}]
                    AFTER INSERT, UPDATE
                    AS
                    BEGIN
                        DECLARE @Operation VARCHAR(10);
                        
                        IF EXISTS(SELECT 1 FROM deleted) 
                            SET @Operation = 'UPDATE';
                        ELSE 
                            SET @Operation = 'INSERT';

                        IF EXISTS(SELECT 1 FROM inserted)
                        BEGIN
                            INSERT INTO [22180008].[Logs_22180008] (TableName, OperationType, OperationDate)
                            VALUES ('{table}', @Operation, GETDATE());
                        END
                    END
                ");
            }
        }
    }
}
