namespace Catalog.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }

        // Відношення один до багатьох (OrderItem)
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
