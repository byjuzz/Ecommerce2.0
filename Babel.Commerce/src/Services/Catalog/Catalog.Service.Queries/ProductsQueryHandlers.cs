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
            var collection = await _context.Products.Where(
            x => request.Products == null || request.Products.Contains(x.ProductId)).OrderByDescending(x => x.ProductId)
                 .GetPagedAsync(request.Page, request.Take);

            return collection.MapTo<DataCollection<ProductDto>>();
        }
    }
}
