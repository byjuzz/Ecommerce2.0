﻿using Catalog.Domain;
using Catalog.Persistence.Database;
using Catalog.Service.EventHandlers.Commands;
using MediatR;

namespace Catalog.Service.EventHandlers
{
    //public class ProductCreate2EventHandler : INotificationHandler<ProductCreateCommand>
    //{
    //    private readonly ApplicationDbContext _context;
    //    public ProductCreate2EventHandler(ApplicationDbContext context)
    //    {
    //        _context = context;
    //    }
    //    public async Task Handle(ProductCreateCommand command, CancellationToken cancellationToken)
    //    {
    //        await _context.AddAsync(new Product
    //        {
    //            Name = command.Name,
    //            Description = command.Description,
    //            Price = command.Price
    //        });
    //        await _context.SaveChangesAsync();
    //    }
    //}
}
