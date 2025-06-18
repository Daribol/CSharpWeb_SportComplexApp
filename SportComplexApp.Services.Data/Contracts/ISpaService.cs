using SportComplexApp.Web.ViewModels.Spa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportComplexApp.Services.Data.Contracts
{
    public interface ISpaService
    {
        Task<IEnumerable<SpaServiceViewModel>> GetAllSpaServicesAsync();
        Task<SpaReservationFormViewModel?> GetSpaServiceByIdAsync(int id);
        Task<int> CreateReservationAsync(SpaReservationFormViewModel model, string userId);
        Task<IEnumerable<MySpaReservationViewModel>> GetUserReservationsAsync(string userId);
        Task<SpaDetailsViewModel?> GetSpaDetailsByIdAsync(int id);
        Task CancelReservationAsync(int reservationId, string userId);
        Task<bool> ReservationExistsAsync(int reservationId, string userId);
    }
}
