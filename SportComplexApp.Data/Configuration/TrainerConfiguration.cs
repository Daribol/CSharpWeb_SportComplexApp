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
        }
    }
}
