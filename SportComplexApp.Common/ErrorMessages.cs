using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
