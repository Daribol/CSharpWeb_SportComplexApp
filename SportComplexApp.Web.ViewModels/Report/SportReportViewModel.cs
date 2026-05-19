using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportComplexApp.Web.ViewModels.Report
{
    public class SportReportViewModel
    {
        public string SportName { get; set; } = null!;
        public int TotalReservations { get; set; }
        public int TotalPeople { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
