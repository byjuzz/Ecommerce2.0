using Client.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Client.Service.Queries.Extensions;
using Client.Service.EventHandlers.Extensions;
using Common.Logging;
using Client.Api.Behaviors;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("EcommerceContext"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "Customer")
                ));

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

//ObJetos Cliento
builder.Services.RegisterRequestHandlers();
builder.Services.RegisterEventHandlers();

builder.Services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

var app = builder.Build();

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

app.MapControllers();

app.Run();
