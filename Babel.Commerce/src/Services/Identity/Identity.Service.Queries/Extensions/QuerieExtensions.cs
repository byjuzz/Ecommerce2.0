using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Queries.Extensions
{
    public static class QuerieExtensions
    {
        public static IServiceCollection RegisterRequestIdentityQueries(this IServiceCollection services)
        {
            return services
                .AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(QuerieExtensions).Assembly));
        }
    }
}
