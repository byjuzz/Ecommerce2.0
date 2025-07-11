using Catalog.Domain;
using Catalog.Persistence.Database;
using Catalog.Service.EventHandlers.Commands;
using MediatR;

namespace Catalog.Service.EventHandlers
{
    public class ProductCreateEventHandler : IRequestHandler<ProductCreateCommand,int>
    {
        private readonly ApplicationDbContext _context;
        public ProductCreateEventHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(ProductCreateCommand command, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = command.Name,
                Description = command.Description,
                Price = command.Price
            };
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
            return product.ProductId;
        }
    }
}
