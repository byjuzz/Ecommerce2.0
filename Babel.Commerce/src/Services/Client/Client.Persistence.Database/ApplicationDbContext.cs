using Client.Domain;
using Client.Persistence.Database.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Client.Persistence.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Domain.Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Database schema
            builder.HasDefaultSchema("Client");

            ModelConfig(builder);
        }

        private void ModelConfig(ModelBuilder builder)
        {
            new ClientConfiguration(builder.Entity<Domain.Client>());
        }
    }
}
