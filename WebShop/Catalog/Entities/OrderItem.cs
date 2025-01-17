using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }

        // Обов'язковий зовнішній ключ на таблицю OrderEntity
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]  // Вказуємо зовнішній ключ
        public OrderEntity? Order { get; set; }  // Зв'язок з сутністю OrderEntity

        // Обов'язковий зовнішній ключ на таблицю ProductEntity
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]  // Вказуємо зовнішній ключ
        public ProductEntity? Product { get; set; }  // Зв'язок з сутністю ProductEntity

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
