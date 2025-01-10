using Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Data.Configuration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            // Налаштовуємо зовнішні ключі
            builder.HasKey(oi => oi.Id);  // Вказуємо, що Id є первинним ключем

            builder.HasOne(oi => oi.Order)  // Вказуємо, що OrderItem має один Order
                .WithMany(o => o.OrderItems)  // Один Order має багато OrderItems
                .HasForeignKey(oi => oi.OrderId)  // Вказуємо, що OrderId є зовнішнім ключем
                .OnDelete(DeleteBehavior.Cascade);  // Якщо замовлення видаляється, то і елементи в OrderItems також видаляються

            builder.HasOne(oi => oi.Product)  // Вказуємо, що OrderItem має один Product
                .WithMany(p => p.OrderItems)  // Один Product має багато OrderItems
                .HasForeignKey(oi => oi.ProductId)  // Вказуємо, що ProductId є зовнішнім ключем
                .OnDelete(DeleteBehavior.Restrict);  // Якщо Product видаляється, то елементи в OrderItems не видаляються

            builder.Property(oi => oi.Quantity)  // Налаштовуємо властивість Quantity
                .IsRequired();  // Встановлюємо, що це обов'язкове поле

            builder.Property(oi => oi.Price)  // Налаштовуємо властивість Price
                .IsRequired();  // Встановлюємо, що це обов'язкове поле
        }
    }
}
