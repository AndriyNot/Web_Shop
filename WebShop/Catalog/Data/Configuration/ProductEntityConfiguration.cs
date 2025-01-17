namespace Catalog.Data.Configuration
{
    using Catalog.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            // Відношення між ProductEntity та Brand
            builder.HasOne(p => p.Brand) // Вказуємо, що кожен продукт має один бренд
                   .WithMany(b => b.Products) // Бренд може мати багато продуктів
                   .HasForeignKey(p => p.BrandId) // Вказуємо зовнішній ключ для бренду
                   .OnDelete(DeleteBehavior.Cascade); // Опціонально, налаштовуємо поведінку при видаленні
        }
    }
}
