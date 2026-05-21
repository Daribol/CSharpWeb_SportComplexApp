using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SportComplexApp.Common;
using SportComplexApp.Services.Data.Contracts;
using SportComplexApp.Web.Controllers;
using SportComplexApp.Web.ViewModels.Tournament;
using static SportComplexApp.Common.ErrorMessages.Tournament;
using static SportComplexApp.Common.SuccessfulValidationMessages.Tournament;

namespace SportComplexApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TournamentManagementController : BaseController
    {
        private readonly ITournamentService tournamentService;
        private readonly ISportService sportService;
        private readonly IStringLocalizer<SharedResource> sharedLocalizer;

        public TournamentManagementController(ITournamentService tournamentService, ISportService sportService, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            this.tournamentService = tournamentService;
            this.sportService = sportService;
            this.sharedLocalizer = sharedLocalizer;
        }

        public async Task<IActionResult> All(string? searchQuery = null, string? sport = null, string? sortBy = null)
        {
            var tournaments = await tournamentService.GetAllAsync(searchQuery, sport, sortBy);
            return View(tournaments.Where(t => !t.IsDeleted));
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddTournamentViewModel
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                Sports = await sportService.GetAllAsSelectListAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTournamentViewModel model)
        {
            if (model.StartDate <= DateTime.Now)
            {
                ModelState.AddModelError(nameof(model.StartDate), sharedLocalizer[TournamentStartInPast]);
            }
            if (model.EndDate <= model.StartDate)
            {
                ModelState.AddModelError(nameof(model.EndDate), sharedLocalizer[TournamentEndBeforeStart]);
            }

            if (!ModelState.IsValid)
            {
                model.Sports = await sportService.GetAllAsSelectListAsync();
                return View(model);
            }

            if (await tournamentService.ExistsAsync(model.Name))
            {
                TempData["ErrorMessage"] = sharedLocalizer[TournamentAlreadyExists].Value;
                model.Sports = await sportService.GetAllAsSelectListAsync();
                return View(model);
            }

            await tournamentService.AddAsync(model);
            TempData["SuccessMessage"] = sharedLocalizer[TournamentCreated].Value;
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await tournamentService.GetForEditAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            model.Sports = await sportService.GetAllAsSelectListAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddTournamentViewModel model)
        {
            if (model.StartDate <= DateTime.Now)
            {
                ModelState.AddModelError(nameof(model.StartDate), sharedLocalizer[TournamentStartInPast]);
            }
            if (model.EndDate <= model.StartDate)
            {
                ModelState.AddModelError(nameof(model.EndDate), sharedLocalizer[TournamentEndBeforeStart]);
            }

            if (!ModelState.IsValid)
            {
                model.Sports = await sportService.GetAllAsSelectListAsync();
                return View(model);
            }

            try
            {
                await tournamentService.EditAsync(id, model);
                TempData["SuccessMessage"] = sharedLocalizer[TournamentUpdated].Value;
                return RedirectToAction(nameof(All));
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError(string.Empty, sharedLocalizer["ConcurrencyError"]);
                model.Sports = await sportService.GetAllAsSelectListAsync();
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await tournamentService.GetForDeleteAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await tournamentService.DeleteAsync(id);
            TempData["SuccessMessage"] = sharedLocalizer[TournamentDeleted].Value;
            return RedirectToAction(nameof(All));
        }
    }
}
