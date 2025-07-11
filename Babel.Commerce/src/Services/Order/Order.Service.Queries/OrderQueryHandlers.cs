using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Persistence.Database;
using Order.Service.Queries.DTOs;
using Service.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Service.Queries
{
    public class OrderQueryHandlers:IRequestHandler<GetOrderQuery, OrderDto> 
    {
        private readonly ApplicationDbContext _context;
        public OrderQueryHandlers(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OrderDto> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            return (await _context.Orders.Include(x => x.Items).SingleAsync(x => x.OrderId == request.Id)).MapTo<OrderDto>();

        }
    }
}
