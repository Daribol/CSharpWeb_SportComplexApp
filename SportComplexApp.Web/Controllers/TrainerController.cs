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
