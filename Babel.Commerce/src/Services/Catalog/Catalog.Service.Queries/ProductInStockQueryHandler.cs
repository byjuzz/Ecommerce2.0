using Azure;
using Catalog.Domain;
using Catalog.Persistence.Database;
using Catalog.Service.Queries.DTO;
using MediatR;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Service.Queries
{
    public class ProductInStockQueryHandler:IRequestHandler<GetProductInStockQuery, DataCollection<ProductInStockDto>>
    {
        private readonly ApplicationDbContext _context;
        public ProductInStockQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DataCollection<ProductInStockDto>> Handle(GetProductInStockQuery request, CancellationToken cancellationToken)
        {
            var collection = await _context.Stocks
            .Where(x => request.Products == null || request.Products.Contains(x.ProductId))
            .OrderBy(x => x.ProductId)
                .GetPagedAsync(request.Page,request.Take);

            return collection.MapTo<DataCollection<ProductInStockDto>>();
        }
    }
}
