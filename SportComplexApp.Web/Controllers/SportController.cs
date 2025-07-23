using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportComplexApp.Common;
using SportComplexApp.Services.Data.Contracts;
using SportComplexApp.Web.ViewModels.Sport;

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
        public async Task<IActionResult> All()
        {
            var sports = await this.sportService.GetAllSportsAsync();
            return View(sports);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Reserve(int id)
        {
            var model = await sportService.GetReservationFormAsync(id);
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
                var fallback = await sportService.GetReservationFormAsync(model.SportId);
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

                var fallback = await sportService.GetReservationFormAsync(model.SportId);
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
