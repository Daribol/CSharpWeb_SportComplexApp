using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportComplexApp.Services.Data.Contracts;
using static SportComplexApp.Common.ErrorMessages.Sport;
using static SportComplexApp.Common.SuccessfulValidationMessages.Sport;

namespace SportComplexApp.Web.Controllers
{
    public class SportController : BaseController
    {
        private readonly ISportService sportService;

        public SportController(ISportService sportService)
        {
            this.sportService = sportService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string? searchQuery = null, int? minDuration = null, int? maxDuration = null)
        {
            var model = await sportService.GetAllSportsAsync(searchQuery, minDuration, maxDuration);
            return View(model);
        }

        public async Task<IActionResult> MySports()
        {
            var userId = GetUserId();

            var model = await sportService.GetMySportsAsync(userId);
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var model = await sportService.GetSportDetailsAsync(id);

            if (model == null)
            {
                TempData["ErrorMessage"] = SportNotFound;
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> AddToMySports(int id)
        {
            var sport = await sportService.GetSportByIdAsync(id);

            if (sport == null)
            {
                TempData["ErrorMessage"] = SportNotFound;
                return RedirectToAction(nameof(Details), new { id });
            }

            var userId = GetUserId();

            try
            {
                await sportService.AddToMySportsAsync(userId, sport);
                TempData["SuccessMessage"] = SportAddedToMyList;
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Details), new { id });
            }

            return RedirectToAction(nameof(MySports));
        }

        public async Task<IActionResult> RemoveFromMySports(int id)
        {
            var sport = await sportService.GetSportByIdAsync(id);

            if (sport == null)
            {
                TempData["ErrorMessage"] = SportNotFound;
                return RedirectToAction(nameof(MySports));
            }

            var userId = GetUserId();

            try
            {
                await sportService.RemoveFromMySportsAsync(userId, sport);
                TempData["SuccessMessage"] = SportRemovedFromMyList;
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(MySports));
        }

        private string GetUserId()
        {
            return User?.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!;
        }
    }
}
