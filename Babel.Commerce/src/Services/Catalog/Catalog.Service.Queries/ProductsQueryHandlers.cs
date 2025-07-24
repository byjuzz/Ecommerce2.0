using Azure;
using Catalog.Persistence.Database;
using Catalog.Service.Queries.DTO;
using MediatR;
using Service.Common.Collection;
using Service.Common.Paging;
using Service.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Service.Queries
{
    public class ProductsQueryHandlers : IRequestHandler<GetProductsQuery, DataCollection<ProductDto>>
    {
        private readonly ApplicationDbContext _context;
        public ProductsQueryHandlers(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<DataCollection<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Products
                .Where(x => request.Products == null || request.Products.Contains(x.ProductId))
                .OrderByDescending(x => x.ProductId);

            var collection = await query.GetPagedAsync(request.Page, request.Take);

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

            return new DataCollection<ProductDto>
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
        }

    }
}
