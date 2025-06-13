using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportComplexApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SportComplexApp.Common.EntityValidationConstants.Facility;

namespace SportComplexApp.Data.Configuration
{
    public class FacilityConfiguration : IEntityTypeConfiguration<Facility>
    {
        public void Configure(EntityTypeBuilder<Facility> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            builder.HasData(SeedFacilities());
        }

        private List<Facility> SeedFacilities()
        {
            List<Facility> facilities = new List<Facility>()
            {
                new Facility()
                {
                    Id = 1,
                    Name = "Main Hall"
                },

                new Facility()
                {
                    Id = 2,
                    Name = "Swimming Pool"
                },

                new Facility()
                {
                    Id = 3,
                    Name = "Tennis Court"
                },
            };

            return facilities;
        }
    }
}
