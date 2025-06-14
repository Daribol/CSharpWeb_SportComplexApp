using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportComplexApp.Data;
using SportComplexApp.Data.Models;
using SportComplexApp.Web.ViewModels.Sport;

namespace SportComplexApp.Web.Controllers
{
    public class SportController : Controller
    {
        private readonly SportComplexDbContext context;

        public SportController(SportComplexDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allSports = await context.Sports
                .Include(s => s.Facility)
                .ToListAsync();

            var viewModel = allSports.Select(s => new AllSportsViewModel
            {
                Name = s.Name,
                ImageUrl = s.ImageUrl ?? string.Empty,
                Duration = s.Duration,
                Price = s.Price,
                FacilityId = s.FacilityId,
                Facility = s.Facility.Name,
            }).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var sport = await this.context.Sports
                .Include(s => s.Facility)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (sport == null)
            {
                return NotFound();
            }
            return View(sport);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Facilities"] = new SelectList(context.Facilities, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sport sport)
        {
            if (ModelState.IsValid)
            {
                context.Add(sport);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Facilities"] = new SelectList(context.Facilities, "Id", "Name");
            return View(sport);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var sport = await this.context.Sports.FindAsync(id);
            if (sport == null)
            {
                return NotFound();
            }

            ViewData["Facilities"] = new SelectList(context.Facilities, "Id", "Name", sport.FacilityId);
            return View(sport);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Sport sport)
        {
            if (id != sport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(sport);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!context.Sports.Any(e => e.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["Facilities"] = new SelectList(context.Facilities, "Id", "Name");
            return View(sport);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var sport = await context.Sports.FindAsync(id);
            if (sport == null)
            {
                return NotFound();
            }

            var model = new DeleteSportViewModel()
            {
                Id = sport.Id,
                Name = sport.Name
            };

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sport = await context.Sports.FindAsync(id);
            if (sport != null)
            {
                context.Sports.Remove(sport);
                await context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
