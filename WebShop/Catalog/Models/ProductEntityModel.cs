namespace Catalog.Entities
{
    public class ProductEntityModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

        // Зовнішній ключ до Category
        public int CategoryId { get; set; }
        public CategoryModel? Category { get; set; }

        // Зовнішній ключ до Brand
        public int BrandId { get; set; }
        public BrandModel? Brand { get; set; }

        // Відношення один до багатьох
        public List<OrderItemModel>? OrderItems { get; set; }

        // Відношення багато до багатьох з користувачами (BuyList, WishList)
        public List<UserModel>? BuyList { get; set; }
        public List<UserModel>? WishList { get; set; }
    }
}
