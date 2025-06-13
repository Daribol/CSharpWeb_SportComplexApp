using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportComplexApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportComplexApp.Data.Configuration
{
    public class TrainerSessionConfiguration : IEntityTypeConfiguration<TrainerSession>
    {
        public void Configure(EntityTypeBuilder<TrainerSession> builder)
        {
            builder.HasKey(ts => ts.Id);

            builder.Property(ts => ts.StartTime)
                .IsRequired();

            builder.Property(ts => ts.EndTime)
                .IsRequired();

            builder.HasOne(ts => ts.Trainer)
                .WithMany(t => t.TrainerSessions)
                .HasForeignKey(ts => ts.TrainerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(SeedTrainerSessions());
        }

        private List<TrainerSession> SeedTrainerSessions()
        {
            List<TrainerSession> trainerSessions = new List<TrainerSession>()
            {
                new TrainerSession
                {
                    Id = 1,
                    TrainerId = 1,
                    StartTime = DateTime.Now.AddHours(1),
                    EndTime = DateTime.Now.AddHours(2)
                },
                new TrainerSession
                {
                    Id = 2,
                    TrainerId = 2,
                    StartTime = DateTime.Now.AddHours(3),
                    EndTime = DateTime.Now.AddHours(4)
                }
            };

            return trainerSessions;
        }
    }
}
