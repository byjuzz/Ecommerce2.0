using Api.Gateway.WebClient.Proxy;
using Api.Gateway.WebClient.Proxy.Config;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Proxies
builder.Services.AddSingleton(new ApiGatewayUrl(builder.Configuration.GetValue<string>("ApiGatewayUrl")));
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient<IOrderProxy, OrderProxy>();
builder.Services.AddHttpClient<IProductProxy, ProductProxy>();
builder.Services.AddHttpClient<IClientProxy, ClientProxy>();

// Add services to the container.
builder.Services.AddRazorPages(o => o.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute()));
builder.Services.AddControllers();

// Add Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapRazorPages();

//Configure Route
app.MapControllerRoute("default", "{controller}/{action=Index}/{id?}");

app.Run();
