namespace Catalog.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int BrandId { get; set; } // Додаємо зовнішній ключ для Brand
        public Brand? Brand { get; set; } // Відношення до Brand

        public List<OrderItem>? OrderItems { get; set; }
        public List<User>? BuyList { get; set; }
        public List<User>? WishList { get; set; }
    }
}
