using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Service.EventHandlers.Extensions
{
    public static class ClientsEventExtensions
    {
        public static IServiceCollection RegisterEventHandlers(this IServiceCollection services)
        {
            return services
                .AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(ClientsEventExtensions).Assembly));
        }
    }
}
