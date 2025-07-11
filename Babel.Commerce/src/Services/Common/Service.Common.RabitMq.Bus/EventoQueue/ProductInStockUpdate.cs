using Service.Common.RabitMq.Bus.EventoQueue.Commands;
using Service.Common.RabitMq.Bus.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Common.RabitMq.Bus.EventoQueue
{
    public class ProductInStockUpdate(ProductInStockUpdateStockCommand data) : Evento
    {
        public ProductInStockUpdateStockCommand Product { get; set; } = data;
    }

}
