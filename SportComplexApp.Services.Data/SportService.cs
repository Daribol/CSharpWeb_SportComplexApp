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

        public async Task<IEnumerable<AllSportsViewModel>> GetAllSportsAsync(string? searchQuery, int? minDuration, int? maxDuration)
        {
            var query = context.Sports
                .Include(s => s.Facility)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(s => s.Name.Contains(searchQuery));
            }

            if (minDuration.HasValue)
            {
                query = query.Where(s => s.Duration >= minDuration.Value);
            }

            if (maxDuration.HasValue)
            {
                query = query.Where(s => s.Duration <= maxDuration.Value);
            }

            return await query
                .Select(s => new AllSportsViewModel
                {
                    Name = s.Name,
                    ImageUrl = s.ImageUrl,
                    Duration = s.Duration,
                    Price = s.Price,
                    Facility = s.Facility.Name,
                    FacilityId = s.FacilityId
                })
                .ToListAsync();
        }

        public async Task<SportsDetailsViewModel?> GetSportDetailsAsync(int id)
        {
            return await context.Sports
                .Include(s => s.Facility)
                .Where(s => s.Id == id)
                .Select(s => new SportsDetailsViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Duration = s.Duration,
                    Price = s.Price,
                    FacilityName = s.Facility.Name,
                    ImageUrl = s.ImageUrl,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<AllSportsViewModel?> GetSportByIdAsync(int id)
        {
            return await context.Sports
                .Include(s => s.Facility)
                .Where(s => s.Id == id)
                .Select(s => new AllSportsViewModel
                {
                    Name = s.Name,
                    ImageUrl = s.ImageUrl,
                    Duration = s.Duration,
                    Price = s.Price,
                    Facility = s.Facility.Name,
                    FacilityId = s.FacilityId
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AllSportsViewModel>> GetMySportsAsync(string userId)
        {
            return await context.Reservations
                .Include(r => r.Sport)
                .ThenInclude(s => s.Facility)
                .Where(r => r.ClientId == userId)
                .Select(r => new AllSportsViewModel
                {
                    Name = r.Sport.Name,
                    Duration = r.Sport.Duration,
                    Price = r.Sport.Price,
                    ImageUrl = r.Sport.ImageUrl,
                    Facility = r.Sport.Facility.Name,
                    FacilityId = r.Sport.FacilityId
                })
                .ToListAsync();
        }

        public async Task AddToMySportsAsync(string userId, AllSportsViewModel sport)
        {
            var sportEntity = await context.Sports.FirstOrDefaultAsync(s => s.Name == sport.Name);

            if (sportEntity == null)
                throw new InvalidOperationException("Sport not found.");

            bool alreadyReserved = await context.Reservations.AnyAsync(r => r.ClientId == userId && r.SportId == sportEntity.Id);
            if (alreadyReserved)
                throw new InvalidOperationException("You have already reserved this sport.");

            context.Reservations.Add(new Reservation
            {
                ClientId = userId,
                SportId = sportEntity.Id,
                ReservationDateTime = DateTime.Now
            });

            await context.SaveChangesAsync();
        }

        public async Task RemoveFromMySportsAsync(string userId, AllSportsViewModel sport)
        {
            var sportEntity = await context.Sports.FirstOrDefaultAsync(s => s.Name == sport.Name);
            if (sportEntity == null)
                throw new InvalidOperationException("Sport not found.");

            var reservation = await context.Reservations
                .FirstOrDefaultAsync(r => r.ClientId == userId && r.SportId == sportEntity.Id);

            if (reservation == null)
                throw new InvalidOperationException("Reservation not found.");

            context.Reservations.Remove(reservation);
            await context.SaveChangesAsync();
        }
    }
}

