using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportComplexApp.Web.ViewModels.Spa
{
    public class SpaReservationFormViewModel
    {
        [Required]
        public int SpaServiceId { get; set; }
        public string SpaServiceName { get; set; } = null!;
        public string? ImageUrl { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Reservation Date and Time")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime ReservationDate { get; set; }

        [Required]
        [DisplayName("Number of People")]
        [Range(1, 10, ErrorMessage = "Number of people must be between 1 and 10.")]
        public int NumberOfPeople { get; set; }
    }
}
