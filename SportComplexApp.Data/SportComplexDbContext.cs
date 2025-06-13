using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportComplexApp.Data.Models;
using System.Reflection;

namespace SportComplexApp.Data
{
    public class SportComplexDbContext : DbContext
    {
        public SportComplexDbContext()
        {

        }
        public SportComplexDbContext(DbContextOptions options)
            : base(options)
        {

        }
       
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<Sport> Sports { get; set; } = null!;
        public virtual DbSet<Facility> Facilities { get; set; } = null!;
        public virtual DbSet<Trainer> Trainers { get; set; } = null!;
        public virtual DbSet<SportTrainer> SportTrainers { get; set; } = null!;
        public virtual DbSet<SpaReservation> SpaReservations { get; set; } = null!;
        public virtual DbSet<TournamentRegistration> TournamentRegistrations { get; set; } = null!;
        public virtual DbSet<SpaService> SpaServices { get; set; } = null!;
        public virtual DbSet<Tournament> Tournaments { get; set; } = null!;
        public virtual DbSet<TrainerSession> TrainerSessions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
