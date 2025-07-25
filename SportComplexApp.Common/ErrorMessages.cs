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
            public const string MaxPeopleLessThanMin = "The maximum number of people must be greater than or equal to the minimum number of people.";
            public const string SportAlreadyExists = "A sport with this name already exists.";
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
            public const string SpaServiceAlreadyExists = "A spa service with this name already exists.";
        }

        public static class Tournament
        {
            public const string TournamentAlreadyRegistered = "You are already registered for this tournament.";
            public const string TournamentRegistrationClosed = "Registration for this tournament is closed.";

            public const string TournamentFull = "This tournament is already full.";
            public const string CannotUnregister = "You cannot unregister from this tournament.";

            public const string TournamentStartInPast = "The start date of the tournament cannot be in the past.";
            public const string TournamentEndBeforeStart = "The end of the tournament cannot be before the start date.";
            public const string TournamentAlreadyExists = "A tournament with this name already exists.";
        }

        public static class Trainer
        {
            public const string TrainerAlreadyExists = "A trainer with this name already exists.";
            public const string MustSelectAtLeastOneSport = "You must select at least one sport for the trainer.";
        }

        public static class Facility
        {
            public const string FacilityAlreadyExists = "A facility with this name already exists.";
            public const string FacilityHasSports = "Cannot delete a facility that has associated sports.";
        }
    }
}
