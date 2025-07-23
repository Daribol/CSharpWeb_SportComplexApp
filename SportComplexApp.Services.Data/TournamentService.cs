﻿using Microsoft.EntityFrameworkCore;
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
                .Where(t => !t.IsDeleted)
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

        public async Task<bool> UnregisterAsync(int tournamentId, string userId)
        {
            var registration = await context.TournamentRegistrations
                .Include(r => r.Tournament)
                .FirstOrDefaultAsync(r => r.TournamentId == tournamentId && r.ClientId == userId);

            if (registration == null || registration.Tournament.StartDate <= DateTime.Now)
            {
                return false;
            }

            context.TournamentRegistrations.Remove(registration);
            await context.SaveChangesAsync();
            return true;
        }


        public async Task<IEnumerable<TournamentViewModel>> GetUserTournamentsAsync(string userId)
        {
            var registrations = await context.TournamentRegistrations
                .Where(tr => tr.ClientId == userId)
                .Include(tr => tr.Tournament)
                .ThenInclude(t => t.Sport)
                .Where(tr => !tr.Tournament.IsDeleted)
                .ToListAsync();

            var pastRegistrations = registrations
                .Where(r => r.Tournament.StartDate < DateTime.Now) 
                .ToList();

            if (pastRegistrations.Any())
            {
                context.TournamentRegistrations.RemoveRange(pastRegistrations);
                await context.SaveChangesAsync();
            }

            return registrations
                .Where(r => r.Tournament.StartDate >= DateTime.Now)
                .Select(r => new TournamentViewModel
                {
                    Id = r.Tournament.Id,
                    Name = r.Tournament.Name,
                    Sport = r.Tournament.Sport.Name,
                    StartDate = r.Tournament.StartDate,
                    Description = r.Tournament.Description,
                })
                .ToList();
        }

        public async Task<bool> IsUserRegisteredAsync(int tournamentId, string userId)
        {
            return await context.TournamentRegistrations
                .AnyAsync(tr => tr.TournamentId == tournamentId && tr.ClientId == userId);
        }

        public async Task AddAsync(AddTournamentViewModel model)
        {
            var tournament = new Tournament
            {
                Name = model.Name,
                Description = model.Description,
                StartDate = model.StartDate,
                SportId = model.SportId,
                IsDeleted = false,
            };

            await context.Tournaments.AddAsync(tournament);
            await context.SaveChangesAsync();
        }

        public async Task<AddTournamentViewModel?> GetForEditAsync(int id)
        {
            var tournament = await context.Tournaments.FindAsync(id);

            if (tournament == null || tournament.IsDeleted)
                return null;

            return new AddTournamentViewModel
            {
                Id = tournament.Id,
                Name = tournament.Name,
                Description = tournament.Description,
                StartDate = tournament.StartDate,
                SportId = tournament.SportId
            };
        }

        public async Task EditAsync(int id, AddTournamentViewModel model)
        {
            var tournament = await context.Tournaments.FindAsync(id);

            if (tournament == null || tournament.IsDeleted)
                return;

            tournament.Name = model.Name;
            tournament.Description = model.Description;
            tournament.StartDate = model.StartDate;
            tournament.SportId = model.SportId;

            await context.SaveChangesAsync();
        }

        public async Task<DeleteTournamentViewModel?> GetForDeleteAsync(int id)
        {
            var tournament = await context.Tournaments
                .Include(t => t.Sport)
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);

            if (tournament == null)
                return null;

            return new DeleteTournamentViewModel
            {
                Id = tournament.Id,
                Name = tournament.Name,
                Sport = tournament.Sport.Name
            };
        }

        public async Task DeleteAsync(int id)
        {
            var tournament = await context.Tournaments.FindAsync(id);

            if (tournament != null && !tournament.IsDeleted)
            {
                tournament.IsDeleted = true;
                await context.SaveChangesAsync();
            }
        }
    }
}
