using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SportComplexApp.Common;
using SportComplexApp.Services.Data.Contracts;
using SportComplexApp.Web.Controllers;
using SportComplexApp.Web.ViewModels.Sport;
using static SportComplexApp.Common.ErrorMessages.Sport;
using static SportComplexApp.Common.SuccessfulValidationMessages.Sport;

namespace SportComplexApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SportManagementController : BaseController
    {
        private readonly ISportService sportService;
        private readonly IStringLocalizer<SharedResource> sharedLocalizer;

        public SportManagementController(ISportService sportService, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            this.sportService = sportService;
            this.sharedLocalizer = sharedLocalizer;
        }

        [HttpGet]
        public async Task<IActionResult> All(string? searchQuery = null, int? minDuration = null, int? maxDuration = null, string? sortBy = null)
        {
            var sports = await this.sportService
                .GetAllSportsAsync(searchQuery, minDuration, maxDuration, sortBy);

            ViewBag.SearchQuery = searchQuery;
            ViewBag.MinDuration = minDuration;
            ViewBag.MaxDuration = maxDuration;
            ViewBag.SortBy = sortBy;

            return View(sports);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = await sportService.GetAddFormModelAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddSportViewModel model)
        {
            if (model.MinPeople > model.MaxPeople)
            {
                ModelState.AddModelError(nameof(model.MaxPeople), sharedLocalizer[MaxPeopleLessThanMin]);
            }

            if (!ModelState.IsValid)
            {
                model.Facilities = await sportService.GetFacilitiesSelectListAsync();
                return View(model);
            }

            if (await sportService.ExistsAsync(model.Name))
            {
                TempData["ErrorMessage"] = sharedLocalizer[SportAlreadyExists].Value;
                model.Facilities = await sportService.GetFacilitiesSelectListAsync();
                return View(model);
            }

            await sportService.AddAsync(model);
            TempData["SuccessMessage"] = sharedLocalizer[SportCreated].Value;
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await sportService.GetSportForEditAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddSportViewModel model)
        {
            if (model.MinPeople > model.MaxPeople)
            {
                ModelState.AddModelError(nameof(model.MaxPeople), sharedLocalizer[MaxPeopleLessThanMin]);
            }

            if (!ModelState.IsValid)
            {
                model.Facilities = await sportService.GetFacilitiesSelectListAsync();
                return View(model);
            }

            try
            {
                await sportService.EditAsync(id, model);
                TempData["SuccessMessage"] = sharedLocalizer[SportUpdated].Value;
                return RedirectToAction(nameof(All));
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError(string.Empty, sharedLocalizer["ConcurrencyError"]);
                model.Facilities = await sportService.GetFacilitiesSelectListAsync();
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await sportService.GetSportForDeleteAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await sportService.DeleteAsync(id);
            TempData["SuccessMessage"] = sharedLocalizer[SportDeleted].Value;
            return RedirectToAction(nameof(All));
        }
    }
}
