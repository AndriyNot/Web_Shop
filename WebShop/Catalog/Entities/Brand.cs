namespace Catalog.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }

        // Відношення один до багатьох
        public List<ProductEntity>? Products { get; set; }
    }
}
