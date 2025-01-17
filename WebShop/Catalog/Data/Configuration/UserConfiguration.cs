namespace Catalog.Data.Configuration
{
    using Catalog.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Налаштування відношень між User і ProductEntity для BuyList
            builder.HasMany(u => u.BuyList)
                .WithMany(p => p.BuyList)
                .UsingEntity(j => j.ToTable("UserBuyList"));

            // Налаштування відношень між User і ProductEntity для WishList
            builder.HasMany(u => u.WishList)
                .WithMany(p => p.WishList)
                .UsingEntity(j => j.ToTable("UserWishList"));
        }
    }
}
