
using Catalog.Api.Manejadores;
using Service.Common.RabitMq.Bus.BusRabbit;
using Service.Common.RabitMq.Bus.EventoQueue;

namespace Catalog.Api.Services
{
    public class ReadRabidMQ(IRabbitEventBus eventBus,ILogger<ReadRabidMQ> logger) : BackgroundService
    {
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            eventBus.Subscribe<ProductInStockUpdate, ManejadorProductInStock>();
            while (!stoppingToken.IsCancellationRequested) 
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //eventBus.StartConsume<ProductInStockUpdate>();
                await Task.Delay(1000,stoppingToken);
            }
        }
    }
}
