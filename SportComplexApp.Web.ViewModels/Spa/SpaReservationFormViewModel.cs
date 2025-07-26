using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static SportComplexApp.Common.EntityValidationConstants.SpaReservation;
using static SportComplexApp.Common.ErrorMessages.SpaReservation;

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
        [DisplayFormat(DataFormatString = ReservationDateTimeFormat, ApplyFormatInEditMode = true)]
        public DateTime ReservationDate { get; set; }

        [Required]
        [DisplayName("Number of People")]
        [Range(1, 10, ErrorMessage = NumberOfPeopleOutOfRange)]
        public int NumberOfPeople { get; set; }
    }
}
