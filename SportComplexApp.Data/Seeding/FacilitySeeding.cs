using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportComplexApp.Data.Models;

namespace SportComplexApp.Data.Seeding;

public class FacilitySeeding : IEntityTypeConfiguration<Facility>
{
    public void Configure(EntityTypeBuilder<Facility> config)
    {
        config.HasData(
            new Facility
            {
                Id = 1,
                Name = "Indoor Arena",
                IsDeleted = false
            },
            new Facility
            {
                Id = 2,
                Name = "Tennis Center",
                IsDeleted = false
            },
            new Facility
            {
                Id = 3,
                Name = "Aquatics & Spa",
                IsDeleted = false
            },
            new Facility
            {
                Id = 4,
                Name = "Fitness Studio",
                IsDeleted = false
            }
        );
    }
}