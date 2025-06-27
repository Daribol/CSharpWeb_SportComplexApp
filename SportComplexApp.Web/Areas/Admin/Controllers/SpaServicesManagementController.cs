using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportComplexApp.Web.Controllers;

namespace SportComplexApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SpaServicesManagementController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
