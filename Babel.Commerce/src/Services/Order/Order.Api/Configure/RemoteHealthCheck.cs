using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Order.Api.Configure
{
    public class RemoteHealthCheck : IHealthCheck
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public RemoteHealthCheck(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var response = await httpClient.GetAsync("http://localhost:5200");
                
                if (response.IsSuccessStatusCode)
                {
                    return HealthCheckResult.Healthy($"Api Catalog is healthy.");
                }
                return HealthCheckResult.Unhealthy($"Api Catalog is Unhealthy.");
            }
        }
    }
}
