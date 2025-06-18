using Microsoft.AspNetCore.Mvc;
using SportComplexApp.Services.Data.Contracts;
using SportComplexApp.Web.ViewModels.Spa;

namespace SportComplexApp.Web.Controllers
{
    public class SpaController : BaseController
    {
        private readonly ISpaService spaService;

        public SpaController(ISpaService spaService)
        {
            this.spaService = spaService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var services = await spaService.GetAllSpaServicesAsync();
            return View(services);
        }

        [HttpGet]
        public async Task<IActionResult> Reserve(int id)
        {
            var model = await spaService.GetSpaServiceByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Reserve(SpaReservationFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = GetUserId();
            await spaService.CreateReservationAsync(model, userId);

            return RedirectToAction(nameof(MyReservations));
        }

        [HttpGet]
        public async Task<IActionResult> MyReservations()
        {
            var userId = GetUserId();
            var reservations = await spaService.GetUserReservationsAsync(userId);
            return View(reservations);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var reservation = await spaService.GetSpaDetailsByIdAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            var userId = GetUserId();
            if (!await spaService.ReservationExistsAsync(id, userId))
            {
                return NotFound();
            }

            await spaService.CancelReservationAsync(id, userId);

            return RedirectToAction(nameof(MyReservations));
        }
    }
}
