using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using Catalog.Data; // Додаємо простір імен для DbInitializer
using Catalog.Services; // Простір імен для сервісів
using Catalog.Repositories; // Простір імен для репозиторіїв
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Catalog.Entities; // Додаємо простір імен для JWT

var builder = WebApplication.CreateBuilder(args);

// Додавання Swagger до DI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WebShop API",
        Version = "v1"
    });
});

builder.Services.AddControllers();

// Налаштування DI для ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Додаємо IPasswordHasher для хешування паролів
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>(); // Реєстрація для хешування паролів

// Підключаємо сервіси
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAuthService, AuthService>(); // Додаємо AuthService

// Підключаємо репозиторії
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Налаштування аутентифікації з JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

var app = builder.Build();

// Автоматичне застосування міграцій та ініціалізація бази даних
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // Застосовуємо всі міграції
    dbContext.Database.Migrate();

    // Ініціалізація бази даних
    DbInitializer.Initialize(dbContext); // Додаємо виклик ініціалізації
}

// Налаштування середовища та маршрутизації
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger(); // Додаємо Swagger Middleware
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        options.RoutePrefix = string.Empty; // Запуск на кореневому шляху
    });
}


app.UseRouting();

// Налаштовуємо аутентифікацію та авторизацію
app.UseAuthentication();
app.UseAuthorization();

// Маршрутизація API
app.MapControllers();

app.Run();
