using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SportComplexApp.Common.EntityValidationConstants.Facility;

namespace SportComplexApp.Web.ViewModels.Facility
{
    using System.ComponentModel.DataAnnotations;

    public class AddFacilityViewModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;
    }
}
