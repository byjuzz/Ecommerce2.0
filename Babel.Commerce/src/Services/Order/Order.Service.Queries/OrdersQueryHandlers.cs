using Azure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Persistence.Database;
using Order.Service.Queries.DTOs;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Service.Queries
{
    public class OrdersQueryHandlers : IRequestHandler<GetOrdersQuery, DataCollection<OrderDto>>
    {
        private readonly ApplicationDbContext _context;

        public OrdersQueryHandlers(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<DataCollection<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var collection = await _context.Orders
                .Include(x => x.Items)
                .OrderByDescending(x => x.OrderId)
                .GetPagedAsync(request.Page, request.Take);

            return collection.MapTo<DataCollection<OrderDto>>();
        }
    }
}
