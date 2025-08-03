using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportComplexApp.Data;
using SportComplexApp.Data.Models;
using SportComplexApp.Services.Data.Contracts;
using SportComplexApp.Web.ViewModels.Home;
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

        // Client-facing methods
        public async Task<IEnumerable<AllTrainersViewModel>> GetAllAsync()
        {
            return await context.Trainers
                .Where(t => !t.IsDeleted)
                .Include(t => t.SportTrainers)
                .ThenInclude(st => st.Sport)
                .Select(t => new AllTrainersViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    LastName = t.LastName,
                    Sports = t.SportTrainers
                        .Where(st => !st.Sport.IsDeleted)
                        .Select(st => st.Sport.Name)
                        .ToList(),
                    ImageUrl = t.ImageUrl
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<TrainerHomeViewModel>> GetAllForHomeAsync()
        {
            return await context.Trainers
                .Where(t => !t.IsDeleted)
                .Select(t => new TrainerHomeViewModel
                {
                    Id = t.Id,
                    FullName = t.Name + " " + t.LastName,
                    ImageUrl = t.ImageUrl
                })
                .ToListAsync();
        }


        public async Task<TrainerDetailsViewModel?> GetTrainerDetailsAsync(int trainerId)
        {
            return await context.Trainers
                .Where(t => !t.IsDeleted && t.Id == trainerId)
                .Include(t => t.SportTrainers)
                .ThenInclude(st => st.Sport)
                .Select(t => new TrainerDetailsViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    LastName = t.LastName,
                    Sports = t.SportTrainers
                        .Select(st => st.Sport.Name)
                        .ToList(),
                    Bio = t.Bio,
                    ImageUrl = t.ImageUrl
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AllTrainersViewModel>> GetTrainersBySportIdAsync(int sportId)
        {
            return await context.Trainers
                .Where(t => !t.IsDeleted && t.SportTrainers.Any(st => st.SportId == sportId))
                .Include(t => t.SportTrainers)
                .ThenInclude(st => st.Sport)
                .Select(t => new AllTrainersViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    LastName = t.LastName,
                    Sports = t.SportTrainers
                        .Select(st => st.Sport.Name)
                        .ToList(),
                    ImageUrl = t.ImageUrl
                })
                .ToListAsync();
        }

        public async Task<int?> GetTrainerIdByUserId(string userId)
        {
            return await context.Trainers
                .Where(t => t.ClientId == userId && !t.IsDeleted)
                .Select(t => (int?)t.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<TrainerReservationViewModel>> GetReservationsForTrainerAsync(int trainerId)
        {
            return await context.Reservations
                .Where(r => r.TrainerId == trainerId)
                .Include(r => r.Client)
                .Include(r => r.Sport)
                .OrderBy(r => r.ReservationDateTime)
                .Select(r => new TrainerReservationViewModel
                {
                    Id = r.Id,
                    ClientName = r.Client.FirstName + " " + r.Client.LastName,
                    SportName = r.Sport.Name,
                    ReservationDate = r.ReservationDateTime,
                    Duration = r.Duration,
                    NumberOfPeople = r.NumberOfPeople
                })
                .ToListAsync();
        }

        // Admin CRUD operations

        public async Task<IEnumerable<SelectListItem>> GetSportsAsSelectListAsync()
        {
            return await context.Sports
                .Where(s => !s.IsDeleted)
                .OrderBy(s => s.Name)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                })
                .ToListAsync();
        }
        public async Task AddAsync(AddTrainerViewModel model)
        {
            var trainer = new Trainer
            {
                Name = model.Name,
                LastName = model.LastName,
                Bio = model.Bio,
                ImageUrl = model.ImageUrl,
                SportTrainers = model.SelectedSportIds
                .Select(sportId => new SportTrainer
                {
                    SportId = sportId
                })
                .ToList()
            };

            await context.Trainers.AddAsync(trainer);
            await context.SaveChangesAsync();
        }

        public async Task<AddTrainerViewModel> GetAddTrainerFormModelAsync()
        {
            var sports = await GetSportsAsSelectListAsync();

            return new AddTrainerViewModel
            {
                AvailableSports = sports
            };
        }
        public async Task<AddTrainerViewModel?> GetTrainerForEditAsync(int id)
        {
            var trainer = await context.Trainers
                .Include(t => t.SportTrainers)
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);

            if (trainer == null)
            {
                return null;
            }

            var allSports = await GetSportsAsSelectListAsync();

            return new AddTrainerViewModel
            {
                Name = trainer.Name,
                LastName = trainer.LastName,
                Bio = trainer.Bio,
                ImageUrl = trainer.ImageUrl,
                SelectedSportIds = trainer.SportTrainers.Select(st => st.SportId).ToList(),
                AvailableSports = allSports
            };
        }

        public async Task EditAsync(int id, AddTrainerViewModel model)
        {
            var trainer = await context.Trainers
                .Include(t => t.SportTrainers)
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);

            if (trainer != null)
            {
                trainer.Name = model.Name;
                trainer.LastName = model.LastName;
                trainer.Bio = model.Bio;
                trainer.ImageUrl = model.ImageUrl;

                trainer.SportTrainers.Clear();
                foreach (var sportId in model.SelectedSportIds)
                {
                    trainer.SportTrainers.Add(new SportTrainer
                    {
                        TrainerId = trainer.Id,
                        SportId = sportId
                    });
                }

                await context.SaveChangesAsync();
            }
        }


        public async Task<DeleteTrainerViewModel?> GetTrainerForDeleteAsync(int id)
        {
            var trainer = await context.Trainers
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);

            if (trainer == null)
            {
                return null;
            }

            return new DeleteTrainerViewModel
            {
                Id = trainer.Id,
                Name = trainer.Name,
                LastName = trainer.LastName
            };
        }

        public async Task DeleteAsync(int id)
        {
            var trainer = await context.Trainers
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);

            if (trainer != null)
            {
                trainer.IsDeleted = true;
                await context.SaveChangesAsync();
            }
        }
    }
}
