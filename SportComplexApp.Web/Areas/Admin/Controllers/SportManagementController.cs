using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportComplexApp.Services.Data.Contracts;
using SportComplexApp.Web.Controllers;
using SportComplexApp.Web.ViewModels.Sport;

namespace SportComplexApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SportManagementController : BaseController
    {
        private readonly ISportService sportService;

        public SportManagementController(ISportService sportService)
        {
            this.sportService = sportService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var sports = await sportService.GetAllSportsAsync();
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
            if (!ModelState.IsValid)
            {
                model.Facilities = await sportService.GetFacilitiesSelectListAsync();
                return View(model);
            }

            await sportService.AddAsync(model);
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await sportService.GetSportForEditAsync(id);
            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddSportViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Facilities = await sportService.GetFacilitiesSelectListAsync();
                return View(model);
            }

            await sportService.EditAsync(id, model);
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await sportService.GetSportForDeleteAsync(id);
            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await sportService.DeleteAsync(id);
            return RedirectToAction(nameof(All));
        }
    }
}
