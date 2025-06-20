using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportComplexApp.Common;
using SportComplexApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SportComplexApp.Common.EntityValidationConstants.Trainer;

namespace SportComplexApp.Data.Configuration
{
    public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
    {
        public void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            builder.Property(t => t.Specialization)
                .IsRequired()
                .HasMaxLength(SpezializationMaxLength);

            builder.Property(t => t.Bio)
                .IsRequired(false)
                .HasMaxLength(BioMaxLenght)
                .HasDefaultValue(null);

            builder.Property(t => t.ImageUrl)
                .IsRequired(false)
                .HasMaxLength(ImageUrlMaxLength)
                .HasDefaultValue(null);

            builder.HasData(SeedTrainers());
        }

        private List<Trainer> SeedTrainers()
        {
            List<Trainer> trainers = new List<Trainer>()
            {
                new Trainer
                {
                    Id = 1,
                    Name = "John Doe",
                    Specialization = "Fitness",
                    Bio = "Experienced fitness trainer with a passion for helping clients achieve their goals.",
                    ImageUrl = "/images/JohnDoe.jpg"
                },
                new Trainer
                {
                    Id = 2,
                    Name = "Jane Smith",
                    Specialization = "Yoga",
                    Bio = "Certified yoga instructor with over 5 years of experience.",
                    ImageUrl = "/images/JaneSmith.jpg"
                },
            };

            return trainers;
        }
    }
}
