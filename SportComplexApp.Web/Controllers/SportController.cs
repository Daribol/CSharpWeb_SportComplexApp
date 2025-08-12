using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportComplexApp.Common;
using SportComplexApp.Services.Data.Contracts;
using SportComplexApp.Web.ViewModels.Sport;
using static SportComplexApp.Common.ErrorMessages.Sport;

namespace SportComplexApp.Web.Controllers
{
    public class SportController : BaseController
    {
        private readonly ISportService sportService;

        public SportController(ISportService sportService)
        {
            this.sportService = sportService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All(string? searchQuery = null, int? minDuration = null, int? maxDuration = null)
        {
            var sports = await this.sportService
                .GetAllSportsAsync(searchQuery, minDuration, maxDuration);
            return View(sports);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Reserve(int id)
        {
            var userId = GetUserId();
            var model = await sportService.GetReservationFormAsync(id, userId);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Reserve(SportReservationFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var fallback = await sportService.GetReservationFormAsync(model.SportId, GetUserId());
                if (fallback != null)
                {
                    model.SportName = fallback.SportName;
                    model.FacilityName = fallback.FacilityName;
                    model.Trainers = fallback.Trainers;
                    model.MinDuration = fallback.MinDuration;
                    model.MaxDuration = fallback.MaxDuration;
                    model.MinPeople = fallback.MinPeople;
                    model.MaxPeople = fallback.MaxPeople;
                }
                return View(model);
            }

            var userId = GetUserId();

            try
            {
                await sportService.CreateReservationAsync(model, userId);
                TempData["SuccessMessage"] = SuccessfulValidationMessages.Reservation.ReservationCreated;
                return RedirectToAction(nameof(MyReservations));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                var fallback = await sportService.GetReservationFormAsync(model.SportId, GetUserId());
                if (fallback != null)
                {
                    model.SportName = fallback.SportName;
                    model.FacilityName = fallback.FacilityName;
                    model.Trainers = fallback.Trainers;
                    model.MinDuration = fallback.MinDuration;
                    model.MaxDuration = fallback.MaxDuration;
                    model.MinPeople = fallback.MinPeople;
                    model.MaxPeople = fallback.MaxPeople;
                }

                return View(model);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyReservations()
        {
            var userId = GetUserId();
            await sportService.DeleteExpiredReservationsAsync(userId);

            var reservations = await sportService.GetUserReservationsAsync(userId);

            return View(reservations);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Cancel(int id)
        {
            var userId = GetUserId();
            if (!await sportService.ReservationExistsAsync(id, userId))
            {
                return NotFound();
            }

            await sportService.CancelReservationAsync(id, userId);

            TempData["SuccessMessage"] = SuccessfulValidationMessages.Reservation.ReservationDeleted;
            return RedirectToAction(nameof(MyReservations));
        }
    }
}
