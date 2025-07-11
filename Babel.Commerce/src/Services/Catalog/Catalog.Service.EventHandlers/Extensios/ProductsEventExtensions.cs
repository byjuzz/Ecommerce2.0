using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Service.EventHandlers.Extensions
{
    public static class ProductsEventExtensions
    {
        public static IServiceCollection RegisterEventHandlers(this IServiceCollection services)
        {
            return services
                .AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(ProductsEventExtensions).Assembly));
        }
    }
}
