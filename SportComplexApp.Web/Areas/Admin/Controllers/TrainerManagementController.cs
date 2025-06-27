using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportComplexApp.Services.Data.Contracts;
using SportComplexApp.Web.Controllers;
using SportComplexApp.Web.ViewModels.Trainer;

namespace SportComplexApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TrainerManagementController : BaseController
    {
        private readonly ITrainerService trainerService;

        public TrainerManagementController(ITrainerService trainerService)
        {
            this.trainerService = trainerService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var trainers = await trainerService.GetAllAsync();
            return View(trainers);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddTrainerViewModel
            {
                AvailableSports = await trainerService.GetSportsAsSelectListAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTrainerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AvailableSports = await trainerService.GetSportsAsSelectListAsync();
                return View(model);
            }

            await trainerService.AddAsync(model);
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await trainerService.GetTrainerForEditAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            model.AvailableSports = await trainerService.GetSportsAsSelectListAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddTrainerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AvailableSports = await trainerService.GetSportsAsSelectListAsync();
                return View(model);
            }

            await trainerService.EditAsync(id, model);
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var trainer = await trainerService.GetTrainerForDeleteAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }

            return View(trainer);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await trainerService.DeleteAsync(id);
            return RedirectToAction(nameof(All));
        }
    }
}