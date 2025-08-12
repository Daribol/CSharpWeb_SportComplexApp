using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportComplexApp.Services.Data.Contracts;
using static SportComplexApp.Common.ErrorMessages.Tournament;
using static SportComplexApp.Common.SuccessfulValidationMessages.Tournament;

namespace SportComplexApp.Web.Controllers
{
    public class TournamentController : BaseController
    {
        private readonly ITournamentService tournamentService;

        public TournamentController(ITournamentService tournamentService)
        {
            this.tournamentService = tournamentService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All(string? searchQuery = null, string? sport = null)
        {
            var tournaments = await tournamentService.GetAllAsync(searchQuery, sport);
            return View(tournaments);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Register(int id)
        {
            var userId = GetUserId();

            var tournament = await tournamentService.GetByIdAsync(id);

            if (tournament == null)
            {
                return NotFound();
            }

            var today = DateTime.Now.Date;

            if (tournament.StartDate.Date <= today && tournament.EndDate.Date >= today)
            {
                TempData["ErrorMessage"] = TournamentRegistrationClosed;
                return RedirectToAction(nameof(All));
            }

            if (User.IsInRole("Trainer"))
            {
                TempData["ErrorMessage"] = TrainerCannotRegister;
                return RedirectToAction(nameof(All));
            }

            var isRegistered = await tournamentService.IsUserRegisteredAsync(id, userId);

            if (isRegistered)
            {
                TempData["ErrorMessage"] = TournamentAlreadyRegistered;
            }
            else
            {
                await tournamentService.RegisterAsync(id, userId);
                TempData["SuccessMessage"] = TournamentRegistered;
            }
            
            return RedirectToAction(nameof(MyTournaments));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Unregister(int id)
        {
            var userId = GetUserId();

            bool success = await tournamentService.UnregisterAsync(id, userId);

            if (success)
            {
                TempData["SuccessMessage"] = TournamentUnregistered;
            }
            else
            {
                TempData["ErrorMessage"] = CannotUnregister;
            }

            return RedirectToAction(nameof(MyTournaments));
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyTournaments()
        {
            var userId = GetUserId();
            var tournaments = await tournamentService.GetUserTournamentsAsync(userId);
            return View(tournaments);
        }
    }
}
