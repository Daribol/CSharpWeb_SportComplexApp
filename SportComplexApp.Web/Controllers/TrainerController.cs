using Microsoft.AspNetCore.Mvc;
using SportComplexApp.Services.Data.Contracts;

namespace SportComplexApp.Web.Controllers
{
    public class TrainerController : BaseController
    {
        private readonly ITrainerService trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            this.trainerService = trainerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var trainers = await trainerService.GetAllAsync();
            return View(trainers);
        }

        [HttpGet]
        public async Task<IActionResult> All(int sportId)
        {
            var trainers = await trainerService.GetTrainersBySportIdAsync(sportId);

            if (trainers == null || !trainers.Any())
            {
                return View("Empty");
            }

            return View(trainers);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var trainer = await trainerService.GetTrainerDetailsAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }

            return View(trainer);
        }
    }
}
