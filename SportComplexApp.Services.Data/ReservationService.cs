using Microsoft.EntityFrameworkCore;
using SportComplexApp.Data;
using SportComplexApp.Data.Models;
using SportComplexApp.Services.Data.Contracts;
using SportComplexApp.Web.ViewModels.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportComplexApp.Services.Data
{
    public class ReservationService : IReservationService
    {
        private readonly SportComplexDbContext context;

        public ReservationService(SportComplexDbContext context)
        {
            this.context = context;
        }
        //public async Task<int> CreateAsync(ReservationFormViewModel model, string userId)
        //{
        //    var Sport = await context.Sports
        //        .FirstOrDefaultAsync(s => s.Id == model.SportId);

        //    if (Sport == null)
        //    {
        //        throw new ArgumentException("Sport not found.");
        //    }
        //    var reservation = new Reservation
        //    {
        //        SportId = model.SportId,
        //        ClientId = userId,
        //        ReservationDateTime = model.ReservationDate.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute),
                
        //    };
        //}

        public Task<bool> DeleteAsync(int id, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MyReservationViewModel>> GetAllByUserIdAsync(string userId)
        {
            return await context.Reservations
                .Where(r => r.ClientId == userId)
                .Include(r => r.Sport)
                .Select(r => new MyReservationViewModel
                {
                    Id = r.Id,
                    SportName = r.Sport.Name,
                    Date = r.ReservationDateTime,
                    Duration = r.Sport.Duration
                })
                .ToListAsync();
        }
    }
}
