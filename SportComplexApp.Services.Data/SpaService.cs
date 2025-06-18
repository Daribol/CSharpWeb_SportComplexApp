using Microsoft.EntityFrameworkCore;
using SportComplexApp.Data;
using SportComplexApp.Data.Models;
using SportComplexApp.Services.Data.Contracts;
using SportComplexApp.Web.ViewModels.Spa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportComplexApp.Services.Data
{
    public class SpaService : ISpaService
    {
        private readonly SportComplexDbContext context;

        public SpaService(SportComplexDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<SpaServiceViewModel>> GetAllSpaServicesAsync()
        {
            return await context.SpaServices
                .Select(s => new SpaServiceViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Price = s.Price,
                    ImageUrl = s.ImageUrl
                })
                .ToListAsync();
        }

        public async Task<SpaReservationFormViewModel?> GetSpaServiceByIdAsync(int id)
        {
            var service = await context.SpaServices.FindAsync(id);

            if (service == null)
            {
                return null;
            }

            return new SpaReservationFormViewModel
            {
                SpaServiceId = service.Id,
                SpaServiceName = service.Name,
                ImageUrl = service.ImageUrl,
                ReservationDate = DateTime.Now.AddDays(1)
            };
        }

        public async Task<int> CreateReservationAsync(SpaReservationFormViewModel model, string userId)
        {
            var reservation = new SpaReservation()
            {
                SpaServiceId = model.SpaServiceId,
                ReservationDateTime = model.ReservationDate,
                NumberOfPeople = model.NumberOfPeople,
                ClientId = userId
            };

            await context.SpaReservations.AddAsync(reservation);
            await context.SaveChangesAsync();

            return reservation.Id;
        }

        public async Task<IEnumerable<MySpaReservationViewModel>> GetUserReservationsAsync(string userId)
        {
            return await context.SpaReservations
                .Where(r => r.ClientId == userId)
                .Include(r => r.SpaService)
                .Select(r => new MySpaReservationViewModel
                {
                    Id = r.Id,
                    SpaServiceName = r.SpaService.Name,
                    DateTime = r.ReservationDateTime,
                    People = r.NumberOfPeople,
                    TotalPrice = r.SpaService.Price * r.NumberOfPeople
                })
                .ToListAsync();
        }

        public async Task<SpaDetailsViewModel?> GetSpaDetailsByIdAsync(int id)
        {
            return await context.SpaServices
                .Where(s => s.Id == id)
                .Select(s => new SpaDetailsViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    ProcedureDetails = s.ProcedureDetails,
                    Price = s.Price,
                    ImageUrl = s.ImageUrl
                })
                .FirstOrDefaultAsync();
        }

        public async Task CancelReservationAsync(int reservationId, string userId)
        {
            var reservation = await context.SpaReservations
                .FirstOrDefaultAsync(r => r.Id == reservationId && r.ClientId == userId);

            if (reservation != null)
            {
                context.SpaReservations.Remove(reservation);
                await context.SaveChangesAsync();
            }
        }

        public async Task<bool> ReservationExistsAsync(int reservationId, string userId)
        {
            return await context.SpaReservations
                .AnyAsync(r => r.Id == reservationId && r.ClientId == userId);
        }
    }
}
