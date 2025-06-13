using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SportComplexApp.Web.ViewModels;

namespace SportComplexApp.Web.Controllers;

public class HomeController : Controller
{
    public HomeController()
    {

    }

    public IActionResult Index()
    {
        ViewData["Title"] = "Home Page";
        ViewData["Message"] = "Welcome to the Sport Complex Web App!";

        return View();
    }
}
