namespace Catalog.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PasswordHash { get; set; }
        public string? Email { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Відношення багато до багатьох (BuyList, WishList)
        public List<ProductEntity>? BuyList { get; set; }
        public List<ProductEntity>? WishList { get; set; }
    }
}
