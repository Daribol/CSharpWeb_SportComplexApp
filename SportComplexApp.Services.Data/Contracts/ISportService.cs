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
        Task<IEnumerable<AllSportsViewModel>> GetAllSportsAsync(string? searchQuery, int? minDuration, int? maxDuration);

        Task<SportsDetailsViewModel?> GetSportDetailsAsync(int id);

        Task<AllSportsViewModel?> GetSportByIdAsync(int id);

        Task<IEnumerable<AllSportsViewModel>> GetMySportsAsync(string userId);

        Task AddToMySportsAsync(string userId, AllSportsViewModel sport);

        Task RemoveFromMySportsAsync(string userId, AllSportsViewModel sport);
    }
}
