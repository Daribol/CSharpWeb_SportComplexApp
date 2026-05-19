using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportComplexApp.Services.Data.Contracts;
using SportComplexApp.Web.Controllers;
using System.Text;
using System.Text.Json;

namespace SportComplexApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ReportController : BaseController
    {
        private readonly ISportService sportService;
        private readonly ISpaService spaService;

        public ReportController(ISportService sportService, ISpaService spaService)
        {
            this.sportService = sportService;
            this.spaService = spaService;
        }

        [HttpGet]
        public async Task<IActionResult> SportReservations()
        {
            var reportData = await sportService.GetSportReservationsReportAsync();
            return View(reportData);
        }

        [HttpGet]
        public async Task<IActionResult> ExportSportReservationsCsv()
        {
            var reportData = await sportService.GetSportReservationsReportAsync();

            var builder = new StringBuilder();

            builder.AppendLine("Sport Name,Total Reservations,Total Visitors,Total Revenue (BGN)");

            foreach (var item in reportData)
            {
                var safeSportName = item.SportName.Replace(",", "");

                builder.AppendLine($"{safeSportName},{item.TotalReservations},{item.TotalPeople},{item.TotalRevenue:F2}");
            }

            builder.AppendLine($"TOTAL,{reportData.Sum(x => x.TotalReservations)},{reportData.Sum(x => x.TotalPeople)},{reportData.Sum(x => x.TotalRevenue):F2}");

            var fileBytes = Encoding.UTF8.GetBytes(builder.ToString());

            var bom = Encoding.UTF8.GetPreamble();
            var completeFile = bom.Concat(fileBytes).ToArray();

            return File(completeFile, "text/csv", "SportReservationsReport.csv");
        }

        [HttpGet]
        public async Task<IActionResult> SpaReservations()
        {
            var reportData = await spaService.GetSpaReservationsReportAsync();
            return View(reportData);
        }

        [HttpGet]
        public async Task<IActionResult> ExportSpaReservationsCsv()
        {
            var reportData = await spaService.GetSpaReservationsReportAsync();

            var builder = new StringBuilder();

            builder.AppendLine("Spa Service Name,Total Reservations,Total Revenue (BGN)");

            foreach (var item in reportData)
            {
                var safeSpaName = item.SpaServiceName.Replace(",", "");

                builder.AppendLine($"{safeSpaName},{item.TotalReservations},{item.TotalRevenue:F2}");
            }

            builder.AppendLine($"TOTAL,{reportData.Sum(x => x.TotalReservations)},{reportData.Sum(x => x.TotalRevenue):F2}");

            var fileBytes = Encoding.UTF8.GetBytes(builder.ToString());
            var bom = Encoding.UTF8.GetPreamble();
            var completeFile = bom.Concat(fileBytes).ToArray();

            return File(completeFile, "text/csv", "SpaReservationsReport.csv");
        }
    }
}