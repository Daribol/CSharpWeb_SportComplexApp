using Microsoft.AspNetCore.Mvc.Rendering;
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
        //Client-facing
        Task<IEnumerable<AllTrainersViewModel>> GetAllAsync();
        Task<TrainerDetailsViewModel> GetTrainerDetailsAsync(int trainerId);
        Task<IEnumerable<AllTrainersViewModel>> GetTrainersBySportIdAsync(int sportId);

        //Admin CRUD operations
        Task AddAsync(AddTrainerViewModel model);
        Task<AddTrainerViewModel> GetAddTrainerFormModelAsync();
        Task<IEnumerable<SelectListItem>> GetSportsAsSelectListAsync();
        Task EditAsync(int id, AddTrainerViewModel model);
        Task DeleteAsync(int id);

        Task<AddTrainerViewModel> GetTrainerForEditAsync(int id);
        Task<DeleteTrainerViewModel?> GetTrainerForDeleteAsync(int id);
    }
}
