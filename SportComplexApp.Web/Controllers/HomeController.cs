using Microsoft.AspNetCore.Mvc;
using SportComplexApp.Services.Data.Contracts;
using SportComplexApp.Web.ViewModels.Home;

namespace SportComplexApp.Web.Controllers;

public class HomeController : Controller
{
    private readonly ISportService sportService;
    private readonly ITrainerService trainerService;
    private readonly ISpaService spaService;
    public HomeController(ISportService sportService, ITrainerService trainerService, ISpaService spaService)
    {
        this.sportService = sportService;
        this.trainerService = trainerService;
        this.spaService = spaService;
    }

    public async Task<IActionResult> Index()
    {
        var model = new HomePageViewModel
        {
            Sports = await sportService.GetAllForHomeAsync(),
            Trainers = await trainerService.GetAllForHomeAsync(),
            SpaProcedures = await spaService.GetAllForHomeAsync()
        };

        return View(model);
    }

    public IActionResult Error(int? statusCode = null)
    {
        if (statusCode == 404)
        {
            return View("Error404");
        }

        return View("Error500");
    }
}
