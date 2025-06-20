using Microsoft.EntityFrameworkCore;
using SportComplexApp.Data;
using SportComplexApp.Services.Data.Contracts;
using SportComplexApp.Web.ViewModels.Trainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportComplexApp.Services.Data
{
    public class TrainerService : ITrainerService
    {
        private readonly SportComplexDbContext context;

        public TrainerService(SportComplexDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<TrainerViewModel>> GetTrainersBySportIdAsync(int sportId)
        {
            return await context.SportTrainers
            .Where(st => st.SportId == sportId)
            .Select(st => new TrainerViewModel
            {
                Id = st.Trainer.Id,
                FullName = st.Trainer.Name,
                ImageUrl = st.Trainer.ImageUrl,
                Specialization = st.Trainer.Specialization
            })
            .ToListAsync();
        }

        public async Task<TrainerViewModel?> GetTrainerDetailsAsync(int trainerId)
        {
            return await context.Trainers
            .Where(t => t.Id == trainerId)
            .Select(t => new TrainerViewModel
            {
                Id = t.Id,
                FullName = t.Name,
                Bio = t.Bio,
                ImageUrl = t.ImageUrl,
                Specialization = t.Specialization
            })
            .FirstOrDefaultAsync();
        }
    }
}
