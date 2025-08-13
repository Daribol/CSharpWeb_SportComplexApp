using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportComplexApp.Services.Data.Contracts;
using SportComplexApp.Web.ViewModels.Sport;
using System.Security.Claims;

namespace SportComplexApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SportsController : ControllerBase
    {
        private readonly ISportService _sports;
        public SportsController(ISportService sports)
        {
            _sports = sports;
        }

        protected string? GetUserId() => User?.FindFirstValue(ClaimTypes.NameIdentifier);

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AllSportsViewModel>>> GetAll(
            [FromQuery] int? minDuration = null, [FromQuery] int? maxDuration = null)
        {
            var items = await _sports.GetAllSportsAsync(null, minDuration, maxDuration);
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AddSportViewModel>> GetById(int id)
        {
            var vm = await _sports.GetSportForEditAsync(id);
            if (vm == null) return NotFound("Sport not found.");

            vm.Facilities = Enumerable.Empty<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            return Ok(vm);
        }
    }
}
