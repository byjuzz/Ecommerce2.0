using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.EventHandlers.Extensions
{
    public static class EventsExtensions
    {
        public static IServiceCollection RegisterRequestIdentityHandlers(this IServiceCollection services)
        {
            return services
                .AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(EventsExtensions).Assembly));
        }
    }
}
