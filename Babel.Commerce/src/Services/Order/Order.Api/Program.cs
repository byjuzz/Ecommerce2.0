using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Order.Persistence.Database;
using Order.Service.Proxies;
using Common.Logging;
using Common.Logging.Serilog;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using HealthChecks.UI.Configuration;
using Order.Api.Configure;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Order.Service.Proxies.Catalog;
using Order.Service.Queries;
using Order.Service.EventHandlers.Extensions;
using Order.Service.Queries.Extensions;
using Service.Common.RabitMq.Bus.Implement;
using Service.Common.RabitMq.Bus.BusRabbit;
using MediatR;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.SerilogConfiguration();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("EcommerceContext"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "Order")
                ));
// Api Urls
builder.Services.Configure<ApiUrls>(
    opts => builder.Configuration.GetSection("ApiUrls").Bind(opts)
);
// Proxies
builder.Services.AddHttpClient<ICatalogProxy, CatalogHttpProxy>();

// Event handlers
builder.Services.RegisterEventHandlers();

// Query services
//builder.Services.AddTransient<IOrderQueryService, OrderQueryService>();
builder.Services.RegisterRequestHandlers();

//Colas
builder.Services.AddSingleton<IRabbitEventBus, RabbitEventBus>(sb => {
    var scopeFactory = sb.GetRequiredService<IServiceScopeFactory>();
    return new RabbitEventBus(scopeFactory);
});

// Health check
builder.Services.AddHealthChecks();
builder.Services.ConfigureHealthChecks(builder.Configuration);

//Accesor
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();

// Add Authentication
var secretKey = Encoding.UTF8.GetBytes(
    builder.Configuration.GetValue<string>("SecretKey")
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // add JWT Authentication
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer", // must be lower case
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {securityScheme, new string[] { }}
    });
});

var app = builder.Build();

//app.Services.GetRequiredService<ILoggerFactory>().AddSyslog(
//                builder.Configuration.GetValue<string>("Papertrail:host"),
//                builder.Configuration.GetValue<int>("Papertrail:port")
//                );

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using var serviceScope = app.Services.CreateScope();
    using var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
    dbContext?.Database.Migrate();
}

app.MapHealthChecks("/api/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.UseHealthChecksUI(delegate (Options options)
{
    options.UIPath = "/healthcheck-ui";
    //options.AddCustomStylesheet("./HealthCheck/Custom.css");

});
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
