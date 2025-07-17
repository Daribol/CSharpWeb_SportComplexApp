using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportComplexApp.Services.Data.Contracts;
using SportComplexApp.Web.Controllers;
using SportComplexApp.Web.ViewModels.Spa;

namespace SportComplexApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SpaServicesManagementController : BaseController
    {
        private readonly ISpaService spaService;

        public SpaServicesManagementController(ISpaService spaService)
        {
            this.spaService = spaService;
        }

        public async Task<IActionResult> All()
        {
            var model = await spaService.GetAllSpaServicesAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View(new AddSpaServiceViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddSpaServiceViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await spaService.AddAsync(model);
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await spaService.GetForEditAsync(id);
            if (model == null) return NotFound();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddSpaServiceViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await spaService.EditAsync(id, model);
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await spaService.GetForDeleteAsync(id);
            if (model == null) return NotFound();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await spaService.DeleteAsync(id);
            return RedirectToAction(nameof(All));
        }
    }
}
