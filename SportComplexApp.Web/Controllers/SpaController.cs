using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportComplexApp.Services.Data.Contracts;
using SportComplexApp.Web.ViewModels.Spa;
using static SportComplexApp.Common.SuccessfulValidationMessages.SpaReservation;

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
        [AllowAnonymous]
        public async Task<IActionResult> All(string? searchQuery = null, int? minDuration = null, int? maxDuration = null, int page = 1)
        {
            const int spaPerPage = 9;
            const int maxPages = 3;

            var viewModel = await spaService.GetAllSpaServicesPaginationAsync(
                searchQuery, minDuration, maxDuration, page, spaPerPage, maxPages);

            if (page > viewModel.TotalPages && viewModel.TotalPages > 0)
            {
                return RedirectToAction(nameof(All), new
                {
                    searchQuery,
                    minDuration,
                    maxDuration,
                    page = viewModel.TotalPages
                });
            }

            return View(viewModel);
        }

        [HttpGet]
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> Reserve(SpaReservationFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = GetUserId();

            try
            {
                await spaService.CreateReservationAsync(model, userId);
                TempData["SuccessMessage"] = SpaReservationCreated;
                return RedirectToAction(nameof(MyReservations));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyReservations()
        {
            var userId = GetUserId();
            await spaService.DeleteExpiredSpaReservationsAsync(userId);

            var reservations = await spaService.GetUserReservationsAsync(userId);
            return View(reservations);
        }

        [HttpGet]
        [AllowAnonymous]
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
        [Authorize]
        public async Task<IActionResult> Cancel(int id)
        {
            var userId = GetUserId();
            if (!await spaService.ReservationExistsAsync(id, userId))
            {
                return NotFound();
            }

            await spaService.CancelReservationAsync(id, userId);

            TempData["SuccessMessage"] = SpaReservationDeleted;
            return RedirectToAction(nameof(MyReservations));
        }
    }
}
