using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Json;

namespace Common.Logging.Serilog
{
    public static class SerilogExtensions
    {
        public static void SerilogConfiguration(this IHostBuilder host)
        {

            host.UseSerilog((context, loggerConfig) =>
                    {
                        loggerConfig.ReadFrom.Configuration(context.Configuration)
                        .WriteTo.Console()
                        .WriteTo.File(new JsonFormatter(), "Logs/applog-.txt", rollingInterval: RollingInterval.Day)
                        .WriteTo.Seq("http://seq:5341")
                        .Enrich.WithExceptionDetails()
                        .Enrich.FromLogContext()
                        .Enrich.WithEnvironmentName()
                        .Enrich.WithEnvironmentUserName()
                        .Enrich.WithMachineName()
                        .Enrich.WithProperty("ApplicationName", context.HostingEnvironment.ApplicationName);
                    });
        }
    }
}
