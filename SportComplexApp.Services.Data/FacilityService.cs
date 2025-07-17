using Microsoft.EntityFrameworkCore;
using SportComplexApp.Data;
using SportComplexApp.Data.Models;
using SportComplexApp.Services.Data.Contracts;
using SportComplexApp.Web.ViewModels.Facility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportComplexApp.Services.Data
{
    public class FacilityService : IFacilityService
    {
        private readonly SportComplexDbContext context;

        public FacilityService(SportComplexDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<AllFacilitiesViewModel>> GetAllAsync()
        {
            return await context.Facilities
                .Where(f => !f.IsDeleted)
                .Include(f => f.Sports.Where(s => !s.IsDeleted))
                .Select(f => new AllFacilitiesViewModel
                {
                    Id = f.Id,
                    Name = f.Name,
                    SportCount = f.Sports.Count(s => !s.IsDeleted)
                })
                .ToListAsync();
        }

        public async Task AddAsync(AddFacilityViewModel model)
        {
            var facility = new Facility
            {
                Name = model.Name,
                IsDeleted = false
            };

            await context.Facilities.AddAsync(facility);
            await context.SaveChangesAsync();
        }

        public async Task<AddFacilityViewModel?> GetFacilityForEditAsync(int id)
        {
            var facility = await context.Facilities
                .Where(f => f.Id == id && !f.IsDeleted)
                .FirstOrDefaultAsync();

            if (facility == null) return null;

            return new AddFacilityViewModel
            {
                Name = facility.Name
            };
        }

        public async Task EditAsync(int id, AddFacilityViewModel model)
        {
            var facility = await context.Facilities
                .Where(f => f.Id == id && !f.IsDeleted)
                .FirstOrDefaultAsync();

            if (facility != null)
            {
                facility.Name = model.Name;
                await context.SaveChangesAsync();
            }
        }

        public async Task<DeleteFacilityViewModel?> GetFacilityForDeleteAsync(int id)
        {
            var facility = await context.Facilities
                .Where(f => f.Id == id && !f.IsDeleted)
                .FirstOrDefaultAsync();

            if (facility == null) return null;

            return new DeleteFacilityViewModel
            {
                Id = facility.Id,
                Name = facility.Name
            };
        }

        public async Task DeleteAsync(int id)
        {
            var facility = await context.Facilities
                .Include(f => f.Sports)
                .FirstOrDefaultAsync(f => f.Id == id && !f.IsDeleted);

            if (facility != null && facility.Sports.Count == 0)
            {
                facility.IsDeleted = true;
                await context.SaveChangesAsync();
            }
        }
    }
}
