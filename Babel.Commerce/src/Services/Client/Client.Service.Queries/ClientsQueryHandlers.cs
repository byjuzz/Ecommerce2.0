using Client.Persistence.Database;
using Client.Service.Queries.DTO;
using MediatR;
using Service.Common.Mapping;
using Client.Service.Queries;
using Microsoft.EntityFrameworkCore;
using Service.Common.Collection;
using Service.Common.Paging;

namespace Client.Service.Queries
{
    public class ClientsQueryHandlers : IRequestHandler<GetClientsQuery, DataCollection<ClientDto>>
    {
        private readonly ApplicationDbContext _context;
        public ClientsQueryHandlers(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DataCollection<ClientDto>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            var collection = await _context.Clients.Where(
                       x => request.Clients == null || request.Clients.Contains(x.ClientId)).OrderByDescending(x => x.ClientId)
                            .GetPagedAsync(request.Page, request.Take);

            return collection.MapTo<DataCollection<ClientDto>>();
        }
    }
}
