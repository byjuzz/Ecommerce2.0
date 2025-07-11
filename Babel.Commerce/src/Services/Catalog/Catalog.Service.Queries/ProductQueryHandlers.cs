using Azure.Core;
using Catalog.Persistence.Database;
using Catalog.Service.Queries.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Common.Collection;
using Service.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Service.Queries
{
    public class ProductQueryHandlers : IRequestHandler<GetProductQuery, ProductDto>
    {
        private readonly ApplicationDbContext _context;
        public ProductQueryHandlers(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            return (await _context.Products.SingleAsync(x => x.ProductId == request.Id)).MapTo<ProductDto>();

        }
    }
}
