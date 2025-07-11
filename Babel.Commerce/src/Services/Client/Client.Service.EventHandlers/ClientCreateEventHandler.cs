using Client.Domain;
using Client.Persistence.Database;
using Client.Service.EventHandlers.Commands;
using MediatR;


namespace Client.Service.EventHandlers
{
    public class ClientCreateEventHandler : IRequestHandler<ClientCreateCommand, int>
    {
        private readonly ApplicationDbContext _context;
        public ClientCreateEventHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(ClientCreateCommand command, CancellationToken cancellationToken)
        {
            var client = new Domain.Client
            {
                Name = command.Name,
            };
            await _context.AddAsync(client);
            await _context.SaveChangesAsync();
            return client.ClientId;
        }
    }
}
