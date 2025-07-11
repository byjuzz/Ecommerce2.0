using Catalog.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Persistence.Database.Configurtation
{
    public class ProductConfiguration
    {
        public ProductConfiguration(EntityTypeBuilder<Product>entityBuilder)
        {
            entityBuilder.HasIndex(x=>x.ProductId);
            entityBuilder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            entityBuilder.Property(X => X.Description).IsRequired().HasMaxLength(500);
            entityBuilder.Property(x => x.Price).HasPrecision(18, 4);
            //Por defecto
            var products = new List<Product>();
            var random = new Random();

            for (int i = 1; i <= 100; i++)
            {
                products.Add(new Product { 
                ProductId=i,
                    Name =$"Product{ i}",
                    Description=$" Description for product {i}",
                    Price=random.Next(50,1000)
                });
            }
            entityBuilder.HasData(products);
        }
    }
}
