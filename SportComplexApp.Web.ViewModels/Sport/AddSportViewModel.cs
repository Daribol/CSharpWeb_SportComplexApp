using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SportComplexApp.Web.ViewModels.Sport
{
    public class AddSportViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [Range(0.01, 1000)]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        [Range(15, 300)]
        public int Duration { get; set; }

        [Required]
        [Display(Name = "Facility")]
        public int FacilityId { get; set; }

        public IEnumerable<SelectListItem> Facilities { get; set; } = new List<SelectListItem>();
    }
}
