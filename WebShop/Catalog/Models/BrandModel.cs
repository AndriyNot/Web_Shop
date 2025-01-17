namespace Catalog.Entities
{
    public class BrandModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }

        // Відношення один до багатьох
        public List<ProductEntityModel>? Products { get; set; }
    }
}
