using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportComplexApp.Web.ViewModels.Report
{
    public class SpaReportViewModel
    {
        public string SpaServiceName { get; set; } = null!;
        public int TotalReservations { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
