using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SportComplexApp.Common.EntityValidationConstants.Sport;

namespace SportComplexApp.Web.ViewModels.Sport
{
    public class AddSportViewModel
    {
        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue)]
        public int FacilityId { get; set; }

        [Required]
        [Range((double)PriceMinValue, (double)PriceMaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(DurationMinValue, DurationMaxValue)]
        public int Duration { get; set; }

        [MaxLength(ImageUrlMaxLength)]
        public string? ImageUrl { get; set; }
    }
}
