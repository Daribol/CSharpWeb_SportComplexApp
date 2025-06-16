﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportComplexApp.Web.ViewModels.Reservation
{
    public class MyReservationViewModel
    {
        public int Id { get; set; }

        public string SportName { get; set; } = null!;

        public DateTime Date { get; set; }

        public int Duration { get; set; }
    }
}
