using Microsoft.AspNetCore.Mvc.Rendering;
using SportComplexApp.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportComplexApp.Web.ViewModels.Reservation
{
    public class ReservationFormViewModel
    {
        [Required]
        [Display(Name = "Sport")]
        public int SportId { get; set; }

        [Required]
        [Display(Name = "Reservation Date")]
        public DateTime ReservationDate { get; set; }

        public IEnumerable<SelectListItem> Sports { get; set; } = new List<SelectListItem>();
    }
}
