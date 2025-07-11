using Catalog.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Persistence.Database.Configurtation
{
    public class ProductInStockConfiguration
    {
        public ProductInStockConfiguration(EntityTypeBuilder<ProductInStock> entityBuilder)
        {
            entityBuilder.HasKey(x => x.ProductInStockId);

            //Data default

            var random = new Random();

            var stocks = new List<ProductInStock>();

            for (int i = 1; i <= 100; i++)
            {
                stocks.Add(new ProductInStock
                {
                    ProductId = i,
                    ProductInStockId = i,
                    Stock = random.Next(0, 50)
                }
                );
            }

            entityBuilder.HasData(stocks);

        }
    }
}
