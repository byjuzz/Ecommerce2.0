using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Service.Queries.DTOs;
using MediatR;
using Service.Common.Collection;

namespace Order.Service.Queries
{
    
        public record GetOrdersQuery(int Page, int Take, IEnumerable<int> Orders = null) : IRequest<DataCollection<OrderDto>>;
        public record GetOrderQuery(int Id) : IRequest<OrderDto>;
    
}
