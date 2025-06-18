using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SportComplexApp.Common.EntityValidationConstants.Sport;

namespace SportComplexApp.Web.ViewModels.Sport
{
    public class SportReservationFormViewModel
    {
        public int SportId { get; set; }

        public string SportName { get; set; } = null!;

        public string FacilityName { get; set; } = null!;

        [Required]
        [Display(Name = "Reservation Date and Time")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime ReservationDateTime { get; set; }

        [Required]
        [Range(15, 300)]
        public int Duration { get; set; }

        [Required]
        [Range(1, 20)]
        [Display(Name = "Number of People")]
        public int NumberOfPeople { get; set; }

        public int? TrainerId { get; set; }

        public IEnumerable<TrainerDropdownViewModel> Trainers { get; set; } = new List<TrainerDropdownViewModel>();
    }
}
