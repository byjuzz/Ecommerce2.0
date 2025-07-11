using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Runtime.CompilerServices;

namespace Order.Api.Configure
{
    public static class HealthCheck
    {
        public static void ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddSqlServer(configuration["ConnectionStrings:EcommerceContext"], healthQuery: "select 1", name: "SQL Server", failureStatus: HealthStatus.Unhealthy, tags: new[] { "Ecommerce", "Database" })
                .AddCheck<RemoteHealthCheck>("Remote endpoints Health Check", failureStatus: HealthStatus.Unhealthy)
                .AddCheck<MemoryHealthCheck>($"Feedback Service Memory Check", failureStatus: HealthStatus.Unhealthy, tags: new[] { "Feedback Service" })
                .AddUrlGroup(new Uri("http://localhost:5278/swagger"), name: "base URL", failureStatus: HealthStatus.Unhealthy);

            services.AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(10); //time in seconds between check    
                opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks    
                opt.SetApiMaxActiveRequests(1); //api requests concurrency    
                opt.AddHealthCheckEndpoint("Identity api", "/api/health"); //map health check api    

            })
             .AddInMemoryStorage();

        }
    }
}
