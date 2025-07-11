using Catalog.Service.Queries.DTO;
using MediatR;
using Service.Common.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Service.Queries
{
    public record GetProductsQuery(int Page, int Take, IEnumerable<int> Products = null) :IRequest<DataCollection<ProductDto>>;
    public record GetProductQuery(int Id) : IRequest<ProductDto>;
    public record GetProductInStockQuery(int Page, int Take, IEnumerable<int> Products = null) : IRequest<DataCollection<ProductInStockDto>>;

}
