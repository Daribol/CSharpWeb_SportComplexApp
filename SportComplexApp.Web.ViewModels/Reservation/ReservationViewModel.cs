using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportComplexApp.Web.ViewModels.Reservation
{
    public class ReservationViewModel
    {
        public int Id { get; set; }

        public string SportName { get; set; } = null!;

        public DateTime ReservationDate { get; set; }

        public int Duration { get; set; } // in minutes

        public decimal Price { get; set; }

        public string FacilityName { get; set; } = null!;
    }
}
