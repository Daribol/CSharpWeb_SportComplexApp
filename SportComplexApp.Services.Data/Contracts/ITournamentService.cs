using SportComplexApp.Web.ViewModels.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportComplexApp.Services.Data.Contracts
{
    public interface ITournamentService
    {
        Task<IEnumerable<TournamentViewModel>> GetAllAsync();
        Task RegisterAsync(int tournamentId, string userId);
        Task<IEnumerable<TournamentViewModel>> GetUserTournamentsAsync(string userId);
        Task<bool> IsUserRegisteredAsync(int tournamentId, string userId);
    }
}
