using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportComplexApp.Common
{
    public static class SuccessfulValidationMessages
    {
        public static class Sport
        {
            public const string SportAddedToMyList = "Sport successfully added to your list.";
            public const string SportRemovedFromMyList = "Sport successfully removed from your list.";

            public const string SportCreated = "Sport successfully created.";
            public const string SportUpdated = "Sport successfully updated.";
            public const string SportDeleted = "Sport successfully deleted.";
        }

        public static class Reservation
        {
            public const string ReservationCreated = "Reservation successfully created.";
            public const string ReservationDeleted = "Reservation successfully deleted.";
        }

        public static class SpaReservation
        {
            public const string SpaReservationCreated = "Spa reservation successfully created.";
            public const string SpaReservationDeleted = "Spa reservation successfully deleted.";
        }

        public static class Tournament
        {
            public const string TournamentRegistered = "You have successfully registered for the tournament.";
            public const string TournamentUnregistered = "You have successfully unregistered from the tournament.";
            public const string TournamentCreated = "Tournament successfully created.";
            public const string TournamentUpdated = "Tournament successfully updated.";
            public const string TournamentDeleted = "Tournament successfully deleted.";
        }
    }
}
