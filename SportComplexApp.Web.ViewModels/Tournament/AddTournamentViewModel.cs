using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportComplexApp.Web.ViewModels.Tournament
{
    public class AddTournamentViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; } = null!;

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Sport")]
        public int SportId { get; set; }

        public IEnumerable<SelectListItem> Sports { get; set; } = new List<SelectListItem>();
    }
}
