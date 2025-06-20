using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportComplexApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SportComplexApp.Common.EntityValidationConstants.Tournament;

namespace SportComplexApp.Data.Configuration
{
    public class TournamentConfiguration : IEntityTypeConfiguration<Tournament>
    {
        public void Configure(EntityTypeBuilder<Tournament> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            builder.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(DescriptionMaxLength);

            builder.Property(t => t.StartDate)
                .IsRequired();

            builder.HasData(SeedTournaments());
        }

        private List<Tournament> SeedTournaments()
        {
            List<Tournament> tournaments = new List<Tournament>()
            {
                new Tournament
                {
                    Id = 1,
                    Name = "Summer Cup",
                    Description = "Annual summer tournament for all skill levels.",
                    StartDate = DateTime.Now.AddMonths(1),
                    SportId = 1
                },
                new Tournament
                {
                    Id = 2,
                    Name = "Winter Championship",
                    Description = "Competitive winter tournament with prizes.",
                    StartDate = DateTime.Now.AddMonths(3),
                    SportId = 2
                }
            };
            return tournaments;
        }
    }
}
