using Basket.Services;
using Basket.Services.Interfaces;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Зчитуємо налаштування Redis з appsettings.json
var redisConnectionString = builder.Configuration.GetValue<string>("Redis:ConnectionString");

// Підключення до Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));

// Реєструємо сервіси
builder.Services.AddSingleton<ICacheService, CacheService>();
builder.Services.AddSingleton<IBasketService, BasketService>();

// Додаємо Swagger
builder.Services.AddEndpointsApiExplorer();  // Додаємо підтримку Swagger для API
builder.Services.AddSwaggerGen();  // Додаємо генерацію Swagger документації

builder.Services.AddControllers();

var app = builder.Build();

// Налаштовуємо використання Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Додаємо Swagger UI
    app.UseSwaggerUI();  // Налаштовуємо відображення Swagger UI
}

app.UseRouting();
app.MapControllers();

app.Run();
