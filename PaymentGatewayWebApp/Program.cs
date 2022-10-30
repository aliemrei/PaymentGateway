using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PaymentGatewayWebApp.Models;
using PaymentGatewayWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<PaymentsDatabaseSettings>(
                builder.Configuration.GetSection(nameof(PaymentsDatabaseSettings)));

builder.Services.AddSingleton<IPaymentsDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<PaymentsDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
        new MongoClient(builder.Configuration.GetValue<string>("PaymentsDatabaseSettings:ConnectionString")));

builder.Services.AddSingleton<IPaymentService, PaymentService>();
 
// Add services to the container.
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
