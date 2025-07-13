using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportComplexApp.Services.Data.Contracts;
using SportComplexApp.Web.Controllers;
using SportComplexApp.Web.ViewModels.Facility;

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

            await facilityService.AddAsync(model);
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
        public async Task<IActionResult> Edit(int id, AddFacilityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await facilityService.EditAsync(id, model);
            return RedirectToAction(nameof(All));
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
            await facilityService.DeleteAsync(id);
            return RedirectToAction(nameof(All));
        }
    }
}
