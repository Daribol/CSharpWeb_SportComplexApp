using SportComplexApp.Web.ViewModels.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportComplexApp.Services.Data.Contracts
{
    public interface IReservationService
    {
        Task<IEnumerable<MyReservationViewModel>> GetAllByUserIdAsync(string userId);
        //Task<int> CreateAsync(ReservationFormViewModel model, string userId);
        Task<bool> DeleteAsync(int id, string userId);
    }
}
