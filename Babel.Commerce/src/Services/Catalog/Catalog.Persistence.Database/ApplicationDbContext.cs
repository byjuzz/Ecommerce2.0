using Catalog.Domain;
using Catalog.Persistence.Database.Configurtation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Persistence.Database
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {

        }
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductInStock> Stocks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Database schema
            builder.HasDefaultSchema("Catalog");

            ModelConfig(builder);
        }

        private void ModelConfig(ModelBuilder builder)
        {
            new ProductConfiguration(builder.Entity<Product>());
            new ProductInStockConfiguration(builder.Entity<ProductInStock>());
        }
    }
}
