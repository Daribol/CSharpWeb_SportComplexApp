using SportComplexApp.Web.ViewModels.Trainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportComplexApp.Services.Data.Contracts
{
    public interface ITrainerService
    {
        Task<IEnumerable<TrainerViewModel>> GetTrainersBySportIdAsync(int sportId);
        Task<TrainerViewModel?> GetTrainerDetailsAsync(int trainerId);
    }
}
