using Client.Persistence.Database;
using Client.Service.Queries.DTO;
using MediatR;
using Service.Common.Mapping;
using Client.Service.Queries;
using Microsoft.EntityFrameworkCore;

namespace Client.Service.Queries
{
    public class ClientQueryHandlers : IRequestHandler<GetClientQuery, ClientDto>
    {
        private readonly ApplicationDbContext _context;
        public ClientQueryHandlers(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<ClientDto> Handle(GetClientQuery request, CancellationToken cancellationToken)
        {
            return (await _context.Clients.SingleAsync(x => x.ClientId == request.Id)).MapTo<ClientDto>();

        }
    }
}
