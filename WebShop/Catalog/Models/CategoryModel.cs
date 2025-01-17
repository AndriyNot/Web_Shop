namespace Catalog.Entities
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // Відношення один до багатьох
        public List<ProductEntityModel>? Products { get; set; }
    }
}
