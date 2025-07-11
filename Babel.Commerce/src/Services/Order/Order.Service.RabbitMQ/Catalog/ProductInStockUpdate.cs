using Order.Service.RabbitMQ.Catalog.Commands;
using Service.Common.RabitMq.Bus.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Service.RabbitMQ.Catalog
{
    public  class ProductInStockUpdate: Evento
    {
        public ProductInStockUpdateStockCommand Product { get; set; }
        public ProductInStockUpdate(ProductInStockUpdateStockCommand data) => Product = data;
    }
}
