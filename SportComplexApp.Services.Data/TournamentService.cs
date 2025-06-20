using Microsoft.EntityFrameworkCore;
using SportComplexApp.Data;
using SportComplexApp.Data.Models;
using SportComplexApp.Services.Data.Contracts;
using SportComplexApp.Web.ViewModels.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportComplexApp.Services.Data
{
    public class TournamentService : ITournamentService
    {
        private readonly SportComplexDbContext context;

        public TournamentService(SportComplexDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<TournamentViewModel>> GetAllAsync()
        {
            return await context.Tournaments
                .Include(t => t.Sport)
                .Select(t => new TournamentViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Sport = t.Sport.Name,
                    StartDate = t.StartDate,
                    Description = t.Description
                })
                .ToListAsync();
        }

        public async Task RegisterAsync(int tournamentId, string userId)
        {
            bool alreadyRegistered = await context.TournamentRegistrations
        .AnyAsync(tr => tr.TournamentId == tournamentId && tr.ClientId == userId);

            if (!alreadyRegistered)
            {
                var registration = new TournamentRegistration
                {
                    TournamentId = tournamentId,
                    ClientId = userId
                };

                await context.TournamentRegistrations.AddAsync(registration);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TournamentViewModel>> GetUserTournamentsAsync(string userId)
        {
            return await context.TournamentRegistrations
                .Where(tr => tr.ClientId == userId)
                .Include(tr => tr.Tournament)
                .ThenInclude(t => t.Sport)
                .Select(tr => new TournamentViewModel
                {
                    Id = tr.Tournament.Id,
                    Name = tr.Tournament.Name,
                    Sport = tr.Tournament.Sport.Name,
                    StartDate = tr.Tournament.StartDate,
                    Description = tr.Tournament.Description
                })
                .ToListAsync();
        }

        public async Task<bool> IsUserRegisteredAsync(int tournamentId, string userId)
        {
            return await context.TournamentRegistrations
                .AnyAsync(tr => tr.TournamentId == tournamentId && tr.ClientId == userId);
        }
    }
}
