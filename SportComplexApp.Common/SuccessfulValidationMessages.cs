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
            public const string SportUpdated = "Sport successfully updated.";
            public const string SportDeleted = "Sport successfully deleted.";
        }

        public static class Reservation
        {
            public const string ReservationCreated = "Reservation successfully created.";
            public const string ReservationDeleted = "Reservation successfully deleted.";
        }
    }
}
