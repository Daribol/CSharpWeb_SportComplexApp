using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportComplexApp.Services.Data.Contracts;
using SportComplexApp.Web.Controllers;
using SportComplexApp.Web.ViewModels.Facility;
using static SportComplexApp.Common.ErrorMessages.Facility;
using static SportComplexApp.Common.SuccessfulValidationMessages.Facility;

namespace SportComplexApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class FacilityManagementController : BaseController
    {
        private readonly IFacilityService facilityService;

        public FacilityManagementController(IFacilityService facilityService)
        {
            this.facilityService = facilityService;
        }

        public async Task<IActionResult> All()
        {
            var facilities = await facilityService.GetAllAsync();
            return View(facilities);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new AddFacilityViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddFacilityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await facilityService.ExistsAsync(model.Name))
            {
                TempData["ErrorMessage"] = FacilityAlreadyExists;
                return View(model);
            }

            await facilityService.AddAsync(model);
            TempData["SuccessMessage"] = FacilityAdded;
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await facilityService.GetFacilityForEditAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AddFacilityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Тук извиквате вашия сървис за запазване на промените
                await facilityService.EditAsync(id, model);
                return RedirectToAction(nameof(All));
            }
            catch (DbUpdateConcurrencyException)
            {
                // Това е грешката, която EF Core хвърля при конфликт!
                ModelState.AddModelError(string.Empty, "Данните бяха променени от друг потребител, докато вие ги редактирахте. Моля, презаредете страницата и опитайте отново.");
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Възникна неочаквана грешка: " + ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await facilityService.GetFacilityForDeleteAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await facilityService.DeleteAsync(id);
                TempData["SuccessMessage"] = FacilityDeleted;
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(All));
        }
    }
}
