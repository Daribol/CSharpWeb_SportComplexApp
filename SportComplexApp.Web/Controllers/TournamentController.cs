using Microsoft.AspNetCore.Mvc;
using SportComplexApp.Data.Models;
using SportComplexApp.Services.Data.Contracts;
using SportComplexApp.Web.ViewModels.Tournament;

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
        public async Task<IActionResult> All()
        {
            var tournaments = await tournamentService.GetAllAsync();
            return View(tournaments);
        }

        [HttpPost]
        public async Task<IActionResult> Register(int id)
        {
            var userId = GetUserId();
            await tournamentService.RegisterAsync(id, userId);
            return RedirectToAction(nameof(MyTournaments));
        }

        [HttpGet]
        public async Task<IActionResult> MyTournaments()
        {
            var userId = GetUserId();
            var tournaments = await tournamentService.GetUserTournamentsAsync(userId);
            return View(tournaments);
        }
    }
}
