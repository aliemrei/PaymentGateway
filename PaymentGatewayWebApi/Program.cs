using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PaymentGatewayWebApi.Models;
using PaymentGatewayWebApi.Repositories;
using PaymentGatewayWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<PaymentsDatabaseSettings>(
                builder.Configuration.GetSection(nameof(PaymentsDatabaseSettings)));

builder.Services.AddSingleton<IPaymentsDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<PaymentsDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
        new MongoClient(builder.Configuration.GetValue<string>("PaymentsDatabaseSettings:ConnectionString")));


builder.Services.AddSingleton<IPaymentRepository, PaymentRepository>();
builder.Services.AddSingleton<IPaymentService, PaymentService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();
