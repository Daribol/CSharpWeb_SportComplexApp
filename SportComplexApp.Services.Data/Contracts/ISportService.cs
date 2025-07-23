using Microsoft.AspNetCore.Mvc.Rendering;
using SportComplexApp.Web.ViewModels.Sport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportComplexApp.Services.Data.Contracts
{
    public interface ISportService
    {
        Task<IEnumerable<AllSportsViewModel>> GetAllSportsAsync();
        Task<SportReservationFormViewModel?> GetReservationFormAsync(int sportId);
        Task<int> CreateReservationAsync(SportReservationFormViewModel model, string userId);
        Task<IEnumerable<SportReservationViewModel>> GetUserReservationsAsync(string userId);
        Task<bool> ReservationExistsAsync(int reservationId, string userId);
        Task CancelReservationAsync(int reservationId, string userId);
        Task DeleteExpiredReservationsAsync(string userId);

        Task<AddSportViewModel> GetAddFormModelAsync();
        Task<IEnumerable<SelectListItem>> GetFacilitiesSelectListAsync();
        Task AddAsync(AddSportViewModel model);
        Task<AddSportViewModel?> GetSportForEditAsync(int id);
        Task EditAsync(int id, AddSportViewModel model);
        Task<DeleteSportViewModel?> GetSportForDeleteAsync(int id);
        Task DeleteAsync(int id);
        Task<IEnumerable<SelectListItem>> GetAllAsSelectListAsync();

    }
}
