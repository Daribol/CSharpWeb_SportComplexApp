using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportComplexApp.Web.ViewModels.Trainer
{
    public class TrainerMasterViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public string SpecialtySport { get; set; } = null!;

        public IEnumerable<TrainerReservationDetailViewModel> Reservations { get; set; } = new List<TrainerReservationDetailViewModel>();
    }
}
