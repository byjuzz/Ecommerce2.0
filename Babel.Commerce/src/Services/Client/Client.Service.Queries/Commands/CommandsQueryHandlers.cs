using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Service.Queries.DTO;
using MediatR;
using Service.Common.Collection;

namespace Client.Service.Queries
{
    
        public record GetClientsQuery(int Page, int Take, IEnumerable<int> Clients = null) : IRequest<DataCollection<ClientDto>>;
        public record GetClientQuery(int Id) : IRequest<ClientDto>;
    
}
