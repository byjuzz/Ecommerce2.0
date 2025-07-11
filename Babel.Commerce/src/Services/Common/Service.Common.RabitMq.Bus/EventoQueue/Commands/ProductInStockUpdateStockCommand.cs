using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Common.RabitMq.Bus.EventoQueue.Commands
{
    public enum ProductInStockAction
    {
        Add,
        Substract
    }

    public class ProductInStockUpdateStockCommand
    {
        public IEnumerable<ProductInStockUpdateItem> Items { get; set; } = new List<ProductInStockUpdateItem>();
    }

    public class ProductInStockUpdateItem
    {
        public int ProductId { get; set; }
        public int Stock { get; set; }
        public ProductInStockAction Action { get; set; }
    }
}
