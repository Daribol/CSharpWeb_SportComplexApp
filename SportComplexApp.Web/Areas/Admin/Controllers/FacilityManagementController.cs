using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportComplexApp.Services.Data.Contracts;
using SportComplexApp.Web.Controllers;
using SportComplexApp.Web.ViewModels.Facility;
using static SportComplexApp.Common.SuccessfulValidationMessages.Facility;
using static SportComplexApp.Common.ErrorMessages.Facility;

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
                TempData["ErrorMessage"] = FacilityNotFound;
                return RedirectToAction(nameof(All));
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddFacilityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await facilityService.EditAsync(id, model);
            TempData["SuccessMessage"] = FacilityUpdated;
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await facilityService.GetFacilityForDeleteAsync(id);
            if (model == null)
            {
                TempData["ErrorMessage"] = FacilityNotFound;
                return RedirectToAction(nameof(All));
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
