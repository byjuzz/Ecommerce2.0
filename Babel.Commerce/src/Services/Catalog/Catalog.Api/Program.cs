using Catalog.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Catalog.Service.Queries.Extensions;
using Catalog.Service.EventHandlers.Extensions;
using Common.Logging;
using Catalog.Api.Behaviors;
using MediatR;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Service.Common.RabitMq.Bus.BusRabbit;
using Catalog.Api.Manejadores;
using Service.Common.RabitMq.Bus.Implement;
using System.Security.Cryptography.Xml;
using Service.Common.RabitMq.Bus.EventoQueue;
using Service.Common.RabitMq.Bus.EventoQueue.Commands;
using Catalog.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("EcommerceContext"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "Catalog")
                ));

builder.Services.AddControllers();
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

//ObJetos Catalogo
builder.Services.RegisterRequestHandlers();
builder.Services.RegisterEventHandlers();

builder.Services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

//Manejador Colas
builder.Services.AddSingleton<IRabbitEventBus, RabbitEventBus>(sb =>
{
    var scopeFactory = sb.GetRequiredService<IServiceScopeFactory>();
    return new RabbitEventBus(scopeFactory);
});

builder.Services.AddTransient<IEventoManejador<ProductInStockUpdate>, ManejadorProductInStock>();
builder.Services.AddTransient<ManejadorProductInStock>();

//builder.Services.AddHostedService<ReadRabidMQ>();

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

var app = builder.Build();

ConfigureEventBus(app);

app.Services.GetRequiredService<ILoggerFactory>().AddSyslog(
                builder.Configuration.GetValue<string>("Papertrail:host"),
                builder.Configuration.GetValue<int>("Papertrail:port")
                );

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using var serviceScope = app.Services.CreateScope();
    using var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
    dbContext?.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();

static void ConfigureEventBus(WebApplication app)
{
    var eventBus = app.Services.GetRequiredService<IRabbitEventBus>();
    eventBus.Subscribe<ProductInStockUpdate, ManejadorProductInStock>();
}