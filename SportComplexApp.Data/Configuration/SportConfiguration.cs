using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportComplexApp.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static SportComplexApp.Common.EntityValidationConstants.Sport;

namespace SportComplexApp.Data.Configuration
{
    public class SportConfiguration : IEntityTypeConfiguration<Sport>
    {
        public void Configure(EntityTypeBuilder<Sport> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            builder.Property(s => s.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasPrecision(10, 2);

            builder.Property(s => s.ImageUrl)
                .IsRequired(false)
                .HasMaxLength(ImageUrlMaxLength)
                .HasDefaultValue(null);

            builder.Property(s => s.Duration)
                .IsRequired();

            builder.HasOne(s => s.Facility)
                .WithMany(f => f.Sports)
                .HasForeignKey(s => s.FacilityId)
                .OnDelete(DeleteBehavior.Cascade);  

            builder.HasData(SeedSports());
        }

        private List<Sport> SeedSports()
        {
            List<Sport> sports = new List<Sport>()
            {
                new Sport
                {
                    Id = 1,
                    Name = "Tennis",
                    FacilityId = 3,
                    Price = 20.00m,
                    ImageUrl = "/images/Tennis.jpg",
                    Duration = 60
                },
                new Sport
                {
                    Id = 2,
                    Name = "Swimming",
                    FacilityId = 2,
                    Price = 15.00m,
                    ImageUrl = "/images/swimming.jpg",
                    Duration = 45
                },
                new Sport
                {
                    Id = 3,
                    Name = "Football",
                    FacilityId = 1,
                    Price = 10.00m,
                    ImageUrl = "/images/football.jpg",
                    Duration = 45
                },
            };

            return sports;
        }
    }
}
