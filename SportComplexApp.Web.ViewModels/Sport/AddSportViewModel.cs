using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
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

        [Range(1, int.MaxValue, ErrorMessage = "The minimum number of people must be at least 1.")]
        public int MinPeople { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The maximum number of people must be at least 1.")]
        public int MaxPeople { get; set; }

        [Required]
        [Display(Name = "Facility")]
        public int FacilityId { get; set; }

        public IEnumerable<SelectListItem> Facilities { get; set; } = new List<SelectListItem>();
    }
}
