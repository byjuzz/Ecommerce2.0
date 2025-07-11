using Catalog.Service.EventHandlers.Commands;
using MediatR;
using Service.Common.Mapping;
using Service.Common.RabitMq.Bus.BusRabbit;
using Service.Common.RabitMq.Bus.EventoQueue;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Catalog.Api.Manejadores
{
    public class ManejadorProductInStock(IMediator _mediator) : IEventoManejador<ProductInStockUpdate>
    {
        
        public async  Task Handle(ProductInStockUpdate @event)
        {
            var data = @event.Product.MapTo<ProductInStockUpdateStockCommand>();
             await _mediator.Publish(data);
            //return Task.CompletedTask;
        }
    }
}
