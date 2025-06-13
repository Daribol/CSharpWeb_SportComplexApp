using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportComplexApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SportComplexApp.Common.EntityValidationConstants.SpaReservation;

namespace SportComplexApp.Data.Configuration
{
    public class SpaReservationConfiguration : IEntityTypeConfiguration<SpaReservation>
    {
        public void Configure(EntityTypeBuilder<SpaReservation> builder)
        {
            builder.HasKey(sr => sr.Id);

            builder.Property(sr => sr.ReservationDateTime)
                .IsRequired();

            builder.Property(sr => sr.NumberOfPeople)
                .IsRequired()
                .HasDefaultValue(1);

            builder.HasOne(sr => sr.Client)
                .WithMany(c => c.SpaReservations)
                .HasForeignKey(sr => sr.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sr => sr.SpaService)
                .WithMany(ss => ss.SpaReservations)
                .HasForeignKey(sr => sr.SpaServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(SeedSpaReservations());
        }

        private List<SpaReservation> SeedSpaReservations()
        {
            List<SpaReservation> spaReservations = new List<SpaReservation>
            {
                new SpaReservation
                {
                    Id = 1,
                    ClientId = "1",
                    SpaServiceId = 1,
                    ReservationDateTime = DateTime.Now.AddDays(1),
                    NumberOfPeople = 2
                },
                new SpaReservation
                {
                    Id = 2,
                    ClientId = "2",
                    SpaServiceId = 2,
                    ReservationDateTime = DateTime.Now.AddDays(2),
                    NumberOfPeople = 3
                }
            };

            return spaReservations;
        }
    }
}
