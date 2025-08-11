using Microsoft.EntityFrameworkCore;
using SportComplexApp.Common;
using SportComplexApp.Data;
using SportComplexApp.Data.Models;
using SportComplexApp.Services.Data.Contracts;
using SportComplexApp.Web.ViewModels.Home;
using SportComplexApp.Web.ViewModels.Spa;
using static SportComplexApp.Common.ErrorMessages.SpaReservation;

namespace SportComplexApp.Services.Data
{
    public class SpaService : ISpaService
    {
        private readonly SportComplexDbContext context;
        private readonly TimeProvider time;

        public SpaService(SportComplexDbContext context, TimeProvider timeProvider)
        {
            this.context = context;
            this.time = timeProvider ?? TimeProvider.System;
        }

        public async Task<IEnumerable<SpaServiceViewModel>> GetAllSpaServicesAsync()
        {
            return await context.SpaServices
                .Where(s => !s.IsDeleted)
                .Select(s => new SpaServiceViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Duration = s.Duration,
                    Price = s.Price,
                    ImageUrl = s.ImageUrl
                })
                .AsNoTracking()
                .ToListAsync();
        }


        public async Task<PaginationSpaServiceViewModel> GetAllSpaServicesPaginationAsync(
            string? searchQuery = null, 
            int? minDuration = null, 
            int? maxDuration = null,
            int currentPage = 1,
            int spaPerPage = 9,
            int maxPages = 3)
        {
            var query = context.SpaServices
                .Where(s => !s.IsDeleted)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query
                    .Where(s => s.Name.ToLower().Contains(searchQuery.ToLower().Trim())
                                || s.Description.ToLower().Contains(searchQuery.ToLower().Trim()));
            }

            if (minDuration.HasValue)
            {
                query = query
                    .Where(s => s.Duration >= minDuration.Value);
            }

            if (maxDuration.HasValue)
            {
                query = query
                    .Where(s => s.Duration <= maxDuration.Value);
            }

            var totalCount = await query.CountAsync();

            int totalPages = (int)Math.Ceiling((double)totalCount / spaPerPage);
            totalPages = Math.Min(totalPages, maxPages);

            if (currentPage > totalPages && totalPages > 0)
            {
                currentPage = totalPages;
            }


            var spaServices = await query
                .Skip((currentPage - 1) * spaPerPage)
                .Take(spaPerPage)
                .Select(s => new SpaServiceViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Duration = s.Duration,
                    Price = s.Price,
                    ImageUrl = s.ImageUrl
                })
                .ToListAsync();

            return new PaginationSpaServiceViewModel
            {
                SpaServices = spaServices,
                CurrentPage = currentPage,
                TotalPages = totalPages,
                SearchQuery = searchQuery,
                MinDuration = minDuration,
                MaxDuration = maxDuration,
                PageSize = spaPerPage
            };
        }

        public async Task<IEnumerable<SpaProcedureHomeViewModel>> GetAllForHomeAsync()
        {
            return await context.SpaServices
                .Where(p => !p.IsDeleted)
                .Select(p => new SpaProcedureHomeViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl
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

            var now = time.GetLocalNow().DateTime;

            return new SpaReservationFormViewModel
            {
                SpaServiceId = service.Id,
                SpaServiceName = service.Name,
                ImageUrl = service.ImageUrl,
                ReservationDate = now.AddDays(1)
            };
        }

        public async Task<int> CreateReservationAsync(SpaReservationFormViewModel model, string userId)
        {
            var spaService = await context.SpaServices
                .FirstOrDefaultAsync(s => s.Id == model.SpaServiceId && !s.IsDeleted);

            var reservationTime = model.ReservationDate;
            var now = time.GetLocalNow().DateTime;

            if (reservationTime.TimeOfDay < ApplicationConstants.OpeningTime
                || reservationTime.TimeOfDay >= ApplicationConstants.ClosingTime)
            {
                throw new InvalidOperationException(ReservationOutsideWorkingHours);
            }

            if (reservationTime > now.AddDays(ApplicationConstants.MaxReservationDaysInAdvance))
            {
                throw new InvalidOperationException(ReservationTooFarInFuture);
            }


            if (reservationTime < now)
            {
                throw new InvalidOperationException(ReservationInPast);
            }

            if (reservationTime < now.AddHours(1))
            {
                throw new InvalidOperationException(ReservationTooSoon);
            }

            var startTime = model.ReservationDate;
            var endTime = startTime.AddMinutes(spaService.Duration);

            var isHired = await context.SpaReservations
                .AnyAsync(r =>
                    r.ClientId == userId &&
                    r.ReservationDateTime < endTime &&
                    r.ReservationDateTime.AddMinutes(spaService.Duration) > startTime);

            if (isHired)
            {
                throw new InvalidOperationException(ReservationConflict);
            }

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
                    Duration = r.SpaService.Duration,
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
                    Duration = s.Duration,
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

        public async Task DeleteExpiredSpaReservationsAsync(string userId)
        {
            var now = time.GetLocalNow().DateTime;

            var expiredSpaReservations = await context.SpaReservations
                .Where(r => r.ClientId == userId &&
                            r.ReservationDateTime <= now)
                .ToListAsync();

            if (expiredSpaReservations.Any())
            {
                context.SpaReservations.RemoveRange(expiredSpaReservations);
                await context.SaveChangesAsync();
            }
        }


        public async Task AddAsync(AddSpaServiceViewModel model)
        {
            var spaService = new SportComplexApp.Data.Models.SpaService
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                Duration = model.Duration,
                ProcedureDetails = model.ProcedureDetails
            };

            await context.SpaServices.AddAsync(spaService);
            await context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await context.SpaServices
                .AnyAsync(s => s.Name.ToLower() == name.ToLower().Trim() && !s.IsDeleted);
        }

        public async Task<AddSpaServiceViewModel?> GetForEditAsync(int id)
        {
            var service = await context.SpaServices.FindAsync(id);
            if (service == null || service.IsDeleted)
                return null;

            return new AddSpaServiceViewModel
            {
                Name = service.Name,
                Description = service.Description,
                ProcedureDetails = service.ProcedureDetails,
                Price = service.Price,
                Duration = service.Duration,
                ImageUrl = service.ImageUrl
            };
        }

        public async Task EditAsync(int id, AddSpaServiceViewModel model)
        {
            var service = await context.SpaServices.FindAsync(id);
            if (service == null || service.IsDeleted)
                return;

            service.Name = model.Name;
            service.Description = model.Description;
            service.ProcedureDetails = model.ProcedureDetails;
            service.Price = model.Price;
            service.Duration = model.Duration;
            service.ImageUrl = model.ImageUrl;

            await context.SaveChangesAsync();
        }

        public async Task<DeleteSpaServiceViewModel?> GetForDeleteAsync(int id)
        {
            var service = await context.SpaServices.FindAsync(id);
            if (service == null || service.IsDeleted)
                return null;

            return new DeleteSpaServiceViewModel
            {
                Id = service.Id,
                Name = service.Name
            };
        }

        public async Task DeleteAsync(int id)
        {
            var service = await context.SpaServices.FindAsync(id);
            if (service != null)
            {
                service.IsDeleted = true;
                await context.SaveChangesAsync();
            }
        }

        public async Task<int> GetSpaServicesCountAsync(string? searchQuery, int? minDuration, int? maxDuration)
        {
            var query = context.SpaServices.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchQuery))
                query = query.Where(s => s.Name.Contains(searchQuery));

            if (minDuration.HasValue)
                query = query.Where(s => s.Duration >= minDuration.Value);

            if (maxDuration.HasValue)
                query = query.Where(s => s.Duration <= maxDuration.Value);

            return await query.CountAsync();
        }

    }
}
