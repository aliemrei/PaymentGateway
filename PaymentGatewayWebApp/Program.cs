using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PaymentGatewayWebApp.Clients;
using PaymentGatewayWebApp.Models;
using PaymentGatewayWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddSingleton<IPaymentGatewayApiClient, PaymentGatewayApiClient>();

builder.Services.AddHttpClient<IPaymentGatewayApiClient, PaymentGatewayApiClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7175/api/");
     
}).AddHttpMessageHandler<LoggingHandler>();

builder.Services.AddTransient<LoggingHandler>();

builder.Services.AddSingleton<IPaymentService, PaymentService>();

builder.Services.AddControllersWithViews(opt =>
{
    opt.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
