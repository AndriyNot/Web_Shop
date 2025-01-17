namespace Catalog.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // Відношення один до багатьох
        public List<ProductEntity>? Products { get; set; }
    }
}
