using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportComplexApp.Data;
using SportComplexApp.Data.Models;
using SportComplexApp.Services.Data.Contracts;

namespace SportComplexApp.Web.Controllers
{
    public class TrainerController : BaseController
    {
        private readonly ITrainerService trainerService;
        private readonly UserManager<Client> userManager;
        private readonly SportComplexDbContext context;

        public TrainerController(ITrainerService trainerService, UserManager<Client> userManager, SportComplexDbContext context)
        {
            this.trainerService = trainerService;
            this.userManager = userManager;
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var trainers = await trainerService.GetAllAsync();
            return View(trainers);
        }

        [HttpGet]
        public async Task<IActionResult> All(int sportId)
        {
            var trainers = await trainerService.GetTrainersBySportIdAsync(sportId);

            if (trainers == null || !trainers.Any())
            {
                return View("Empty");
            }

            ViewBag.SportId = sportId;
            return View(trainers);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, int sportId)
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
                return NotFound("Trainer not found.");
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
                TempData["ErrorMessage"] = "Unable to identify trainer.";
                return RedirectToAction(nameof(Reservations));
            }

            var reservation = await context.Reservations
                .FindAsync(reservationId);

            if (reservation == null)
            {
                TempData["ErrorMessage"] = "Reservation not found or you're not authorized to cancel it.";
                return RedirectToAction(nameof(Reservations));
            }

            context.Reservations.Remove(reservation);
            await context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Reservation cancelled successfully.";
            return RedirectToAction(nameof(Reservations));
        }
    }
}
