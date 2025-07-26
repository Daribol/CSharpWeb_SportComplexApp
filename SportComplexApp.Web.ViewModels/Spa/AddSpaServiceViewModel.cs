using System.ComponentModel.DataAnnotations;
using static SportComplexApp.Common.ErrorMessages.SpaService;
using static SportComplexApp.Common.EntityValidationConstants.SpaService;

namespace SportComplexApp.Web.ViewModels.Spa
{
    public class AddSpaServiceViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = NameRequirenments)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = DescriptionRequirenments)]
        public string Description { get; set; } = null!;

        [Required]
        public string ProcedureDetails { get; set; } = null!;

        [Required]
        [Range(5.00, 1000.00, ErrorMessage = PriceTooLow)]
        public decimal Price { get; set; }

        [Required]
        [Range(20, 120)]
        public int Duration { get; set; }

        public string ImageUrl { get; set; } = null!;
    }
}
