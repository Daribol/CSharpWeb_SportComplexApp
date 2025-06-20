using Microsoft.EntityFrameworkCore;
using SportComplexApp.Data;
using SportComplexApp.Data.Models;
using SportComplexApp.Services.Data.Contracts;
using SportComplexApp.Web.ViewModels.Sport;

namespace SportComplexApp.Services
{
    public class SportService : ISportService
    {
        private readonly SportComplexDbContext context;

        public SportService(SportComplexDbContext db)
        {
            this.context = db;
        }

        public async Task<IEnumerable<AllSportsViewModel>> GetAllSportsAsync()
        {
            return await context.Sports
                .Include(s => s.Facility)
                .Select(s => new AllSportsViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Duration = s.Duration,
                    Price = s.Price,
                    Facility = s.Facility.Name,
                    ImageUrl = s.ImageUrl
                })
                .ToListAsync();
        }

        public async Task<SportReservationFormViewModel?> GetReservationFormAsync(int sportId)
        {
            var sport = await context.Sports
                .Where(s => s.Id == sportId)
                .Select(s => new SportReservationFormViewModel
                {
                    SportId = s.Id,
                    SportName = s.Name,
                    FacilityName = s.Facility.Name,
                    ReservationDateTime = DateTime.Now.AddDays(1),
                })
                .FirstOrDefaultAsync();

            if (sport == null)
            {
                return null;
            }

            sport.Trainers = await context.SportTrainers
                .Where(st => st.SportId == sportId)
                .Select(st => new TrainerDropdownViewModel
                {
                    Id = st.TrainerId,
                    FullName = st.Trainer.Name
                })
                .ToListAsync();

            return sport;
        }

        public async Task<int> CreateReservationAsync(SportReservationFormViewModel model, string userId)
        {
            var reservation = new Reservation
            {
                ClientId = userId,
                SportId = model.SportId,
                TrainerId = model.TrainerId,
                Duration = model.Duration,
                NumberOfPeople = model.NumberOfPeople,
                ReservationDateTime = model.ReservationDateTime
            };

            await context.Reservations.AddAsync(reservation);
            await context.SaveChangesAsync();

            return reservation.Id;
        }

        public async Task<IEnumerable<SportReservationViewModel>> GetUserReservationsAsync(string userId)
        {
            return await context.Reservations
                .Where(r => r.ClientId == userId)
                .Include(r => r.Sport).ThenInclude(s => s.Facility)
                .Include(r => r.Trainer)
                .Select(r => new SportReservationViewModel
                {
                    Id = r.Id,
                    SportName = r.Sport.Name,
                    FacilityName = r.Sport.Facility.Name,
                    Duration = r.Duration,
                    ReservationDateTime = r.ReservationDateTime,
                    NumberOfPeople = r.NumberOfPeople,
                    TrainerName = r.Trainer != null ? r.Trainer.Name : "No Trainer",
                    TotalPrice = Math.Round(r.Sport.Price * (r.Duration/60m) * r.NumberOfPeople, 2)
                })
                .ToListAsync();
        }

        public async Task<bool> ReservationExistsAsync(int reservationId, string userId)
        {
            return await context.Reservations
                .AnyAsync(r => r.Id == reservationId && r.ClientId == userId);
        }

        public async Task CancelReservationAsync(int reservationId, string userId)
        {
            var reservation = await context.Reservations
                .FirstOrDefaultAsync(r => r.Id == reservationId && r.ClientId == userId);
            if (reservation != null)
            {
                context.Reservations.Remove(reservation);
                await context.SaveChangesAsync();
            }
        }
    }
}

