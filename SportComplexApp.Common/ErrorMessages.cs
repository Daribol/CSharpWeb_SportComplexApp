using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SportComplexApp.Common
{
    public static class ErrorMessages
    {
        public static class Sport
        {
            public const string SportNotFound = "Sport not found.";
            public const string SportAlreadyExists = "Sport already exists in your list.";
            public const string SportNotInList = "Sport is not in your list.";
        }

        public static class Reservation
        {
            public const string ReservationTooSoon = "You can only make a reservation at least 1 hour in advance.";
            public const string ReservationInPast = "Reservation date cannot be in the past.";
            public const string ReservationConflict = "You already have a reservation during this time.";
        }

        public static class  SpaReservation
        {
            public const string PriceTooLow = "The price must be at least 5.00.";
            public const string ReservationInPast = "You cannot reserve a spa procedure in the past.";
            public const string ReservationTooSoon = "You must reserve at least 1 hour in advance.";
            public const string ReservationConflict = "You already have a reservation during this time.";
        }

        public static class Tournament
        {
            public const string TournamentAlreadyRegistered = "You are already registered for this tournament.";
            public const string TournamentRegistrationClosed = "Registration for this tournament is closed.";
            public const string TournamentFull = "This tournament is already full.";
            public const string CannotUnregister = "You cannot unregister from this tournament.";
        }
    }
}
