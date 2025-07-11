using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Client.Service.Queries.Extensions
{
    public static class ClientExtensions
    {
        public static IServiceCollection RegisterRequestHandlers(this IServiceCollection services)
        {
            return services
                .AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(ClientExtensions).Assembly));
        }
    }
}
