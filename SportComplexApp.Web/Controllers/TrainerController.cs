using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SportComplexApp.Common;
using SportComplexApp.Data;
using SportComplexApp.Data.Models;
using SportComplexApp.Services;
using SportComplexApp.Services.Data.Contracts;
using SportComplexApp.Web.ViewModels.Trainer;

namespace SportComplexApp.Web.Controllers
{
    public class TrainerController : BaseController
    {
        private readonly ITrainerService trainerService;
        private readonly ISportService sportService;
        private readonly UserManager<Client> userManager;
        private readonly SportComplexDbContext context;
        private readonly IStringLocalizer<SharedResource> sharedLocalizer;

        public TrainerController(ITrainerService trainerService, ISportService sportService, UserManager<Client> userManager, SportComplexDbContext context, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            this.trainerService = trainerService;
            this.sportService = sportService;
            this.userManager = userManager;
            this.context = context;
            this.sharedLocalizer = sharedLocalizer;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All(int sportId)
        {
            var trainers = await trainerService.GetTrainersBySportIdAsync(sportId);

            if (trainers == null || !trainers.Any())
            {
                return NotFound();
            }

            ViewBag.SportId = sportId;
            return View(trainers);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Browse(string? q = null, int? sportId = null, string? sortBy = null)
        {
            var model = await trainerService.GetAllPublicAsync(q, sportId, sortBy);
            ViewBag.Sports = await sportService.GetAllAsSelectListAsync();
            ViewBag.Query = q;
            ViewBag.SelectedSportId = sportId;
            ViewBag.SortBy = sortBy;
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id, int? sportId = null)
        {
            var trainer = await trainerService.GetTrainerDetailsAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }

            ViewBag.SportId = sportId;
            return View(trainer);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> AllTrainers()
        {
            var trainersData = await context.Trainers
                .Where(t => t.IsDeleted == false)
                .Select(t => new
                {
                    Id = t.Id,
                    FirstName = t.Name,
                    LastName = t.LastName,
                    SportNames = t.SportTrainers.Select(st => st.Sport.Name).ToList(),
                    Reservations = t.Reservations.Select(r => new
                    {
                        Id = r.Id,
                        RawDateTime = r.ReservationDateTime,
                        Duration = r.Duration
                    }).ToList()
                })
                .ToListAsync();

            var trainers = trainersData.Select(t => new TrainerMasterViewModel
            {
                Id = t.Id,
                FullName = $"{t.FirstName} {t.LastName}",
                SpecialtySport = t.SportNames.Any() ? string.Join(", ", t.SportNames) : "No Specialty",
                Reservations = t.Reservations.Select(r => new TrainerReservationDetailViewModel
                {
                    Id = r.Id,
                    CustomerName = "Booked",
                    ReservationDate = r.RawDateTime.ToString("yyyy-MM-dd"),
                    TimeSlot = $"{r.RawDateTime:HH\\:mm} - {r.RawDateTime.AddMinutes(r.Duration):HH\\:mm}",
                    Status = r.RawDateTime > DateTime.UtcNow ? "Upcoming" : "Completed"
                }).ToList()
            }).ToList();

            return View(trainers);
        }

        [Authorize(Roles = "Trainer")]
        public async Task<IActionResult> Reservations()
        {
            var user = await userManager.GetUserAsync(User);
            var trainerId = await trainerService.GetTrainerIdByUserId(user.Id);

            if (trainerId == null)
            {
                return NotFound();
            }

            var reservations = await trainerService.GetReservationsForTrainerAsync(trainerId.Value);
            return View(reservations);
        }

        [Authorize(Roles = "Trainer")]
        [HttpPost]
        public async Task<IActionResult> CancelReservation(int reservationId)
        {
            var user = await userManager.GetUserAsync(User);
            var trainerId = await trainerService.GetTrainerIdByUserId(user.Id);

            if (trainerId == null)
            {
                return NotFound();
            }

            var reservation = await context.Reservations
                .FindAsync(reservationId);

            if (reservation == null)
            {
                TempData["ErrorMessage"] = sharedLocalizer["CancelReservationUnauthorized"].Value;
                return RedirectToAction(nameof(Reservations));
            }

            context.Reservations.Remove(reservation);
            await context.SaveChangesAsync();

            TempData["SuccessMessage"] = sharedLocalizer[SuccessfulValidationMessages.Reservation.ReservationDeleted].Value;
            return RedirectToAction(nameof(Reservations));
        }
    }
}
