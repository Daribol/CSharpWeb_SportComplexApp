using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportComplexApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SportComplexApp.Common.EntityValidationConstants.Client;

namespace SportComplexApp.Data.Configuration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(FirstNameMaxLength);

            builder.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(LastNameMaxLength);

            builder.HasData(SeedClients());
        }
        private List<Client> SeedClients()
        {
            List<Client> clients = new List<Client>()
            {
                new Client()
                {
                    Id = "1",
                    FirstName = "John",
                    LastName = "Doe"
                },

                new Client()
                {
                    Id = "2",
                    FirstName = "Jane",
                    LastName = "Smith"
                }
            };

            return clients;
        }
    }
}
