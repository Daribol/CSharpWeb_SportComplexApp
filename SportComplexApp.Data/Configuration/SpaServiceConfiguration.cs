using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportComplexApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SportComplexApp.Common.EntityValidationConstants.SpaService;

namespace SportComplexApp.Data.Configuration
{
    public class SpaServiceConfiguration : IEntityTypeConfiguration<SpaService>
    {
        public void Configure(EntityTypeBuilder<SpaService> builder)
        {
            builder.HasKey(ss => ss.Id);

            builder.Property(ss => ss.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            builder.Property(ss => ss.Description)
                .IsRequired()
                .HasMaxLength(DescriptionMaxLength);

            builder.Property(ss => ss.ProcedureDetails)
                .IsRequired();

            builder.Property(ss => ss.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasPrecision(10, 2);

            builder.Property(ss => ss.ImageUrl)
                .HasMaxLength(ImageUrlMaxLength);

            builder.Property(ss => ss.Duration)
                .IsRequired()
                .HasDefaultValue(60);

            builder.HasData(SeedSpaServices());
        }

        private List<SpaService> SeedSpaServices()
        {
            List<SpaService> spaServices = new List<SpaService>()
            {
                new SpaService
                {
                    Id = 1,
                    Name = "Relaxing Massage",
                    Description = "A soothing massage to relieve stress and tension.",
                    ProcedureDetails = "This massage focuses on relaxation and stress relief, using gentle techniques to soothe the body and mind.",
                    Price = 50.00m,
                    ImageUrl = "/images/RelaxingMassage.jpg"
                },
                new SpaService
                {
                    Id = 2,
                    Name = "Facial Treatment",
                    Description = "A rejuvenating facial to enhance your skin's glow.",
                    ProcedureDetails = "This facial treatment includes cleansing, exfoliation, and moisturizing to improve skin texture and appearance.",
                    Price = 70.00m,
                    ImageUrl = "/images/FacialTreatment.jpg"
                },
            };

            return spaServices;
        }
    }
}
