using Microsoft.EntityFrameworkCore;
using Catalog.Entities;
using Catalog.Data.Configuration;


public class ApplicationDbContext : DbContext
{
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<User> Users { get; set; }

    // Додавання конструктора, який приймає DbContextOptions
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Додаємо конфігурації для кожної сутності
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
        modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
        // Додатково можна додавати конфігурації для інших сутностей

        base.OnModelCreating(modelBuilder);
    }
}
