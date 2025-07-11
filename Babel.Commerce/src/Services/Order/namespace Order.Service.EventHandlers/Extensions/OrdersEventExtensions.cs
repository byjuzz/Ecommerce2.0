using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Service.EventHandlers.Extensions
{
    public static class CatalogsEventExtensions
    {
        public static IServiceCollection RegisterEventHandlers(this IServiceCollection services)
        {
            return services
                .AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(CatalogsEventExtensions).Assembly));
        }
    }
}
