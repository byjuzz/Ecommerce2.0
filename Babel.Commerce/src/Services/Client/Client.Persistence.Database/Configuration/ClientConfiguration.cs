using System;
using Client.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Client.Persistence.Database.Configuration
{
    public class ClientConfiguration
    {
        public ClientConfiguration(EntityTypeBuilder<Domain.Client> entityBuilder)
        {
            entityBuilder.HasIndex(x => x.ClientId);
            entityBuilder.Property(x => x.Name).IsRequired().HasMaxLength(250);
        }
    }
}
