using Identity.Domain;
using Identity.Persistence.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Identity.Service.EventHandlers.Extensions;
using Identity.Service.Queries.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("EcommerceContext"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "Identity")
                ));

//Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();

// Identity configuration
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;
});

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

// Event handlers
builder.Services.RegisterRequestIdentityHandlers();
builder.Services.RegisterRequestIdentityQueries();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using var serviceScope = app.Services.CreateScope();
    using var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
    dbContext?.Database.Migrate();

    await Seeding(serviceScope);
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();

static async Task Seeding(IServiceScope serviceScope)
{
    var userManager = serviceScope?.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = serviceScope?.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

    string roleName = "Admin";
    string email = "user@example.com";
    string password = "Babel2025$";

    // Asegurar que el rol "Admin" existe
    if (!await roleManager.RoleExistsAsync(roleName))
    {
        await roleManager.CreateAsync(new ApplicationRole { Name = roleName, NormalizedName = roleName.ToUpper() });
    }

    // Crear usuario si no existe
    var existingUser = await userManager.FindByEmailAsync(email);
    if (existingUser == null)
    {
        var user = new ApplicationUser
        {
            FirstName = "Mauricio",
            LastName = "Sandoval",
            Email = email,
            UserName = email
        };

        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, roleName);
        }
    }
    else
    {
        // Asegurarse que ya tenga el rol asignado (en caso de recrear contenedor sin borrar DB)
        var hasRole = await userManager.IsInRoleAsync(existingUser, roleName);
        if (!hasRole)
        {
            await userManager.AddToRoleAsync(existingUser, roleName);
        }
    }
}
