using Catalog.Persistence.Database;
using Catalog.Service.Queries.DTO;
using Service.Common.Collection;
using Service.Common.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Common.Mapping;

namespace Catalog.Service.Queries
{
    public class ProductQueryService : IProductQueryService
    {
        private readonly ApplicationDbContext _context;
        public ProductQueryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<DataCollection<ProductDto>> GetAllAsync(int page, int take, IEnumerable<int> products = null)
        {
            var productQuery = _context.Products
                .Where(x => products == null || products.Contains(x.ProductId))
                .OrderByDescending(x => x.ProductId);

            var collection = await productQuery.GetPagedAsync(page, take);

            var productIds = collection.Items.Select(p => p.ProductId).ToList();

            var stockDict = await _context.Stocks
                .Where(s => productIds.Contains(s.ProductId))
                .ToDictionaryAsync(
                    s => s.ProductId,
                    s => new ProductInStockDto
                    {
                        ProductInStockId = s.ProductInStockId,
                        ProductId = s.ProductId,
                        Stock = s.Stock
                    });

            var result = new DataCollection<ProductDto>
            {
                Page = collection.Page,
                Pages = collection.Pages,
                Total = collection.Total,
                Items = collection.Items.Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = stockDict.ContainsKey(p.ProductId) ? stockDict[p.ProductId] : null
                }).ToList()
            };

            return result;
        }

        public async Task<ProductDto> GetAsync(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(p => p.ProductId == id);
            if (product == null) return null;

            var stock = await _context.Stocks
                .Where(s => s.ProductId == id)
                .Select(s => new ProductInStockDto
                {
                    ProductInStockId = s.ProductInStockId,
                    ProductId = s.ProductId,
                    Stock = s.Stock
                })
                .FirstOrDefaultAsync();

            return new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = stock
            };
        }
    }
}
