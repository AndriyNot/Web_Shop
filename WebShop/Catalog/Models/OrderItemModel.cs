using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Entities
{
    public class OrderItemModel
    {
        public int Id { get; set; }

        // Обов'язковий зовнішній ключ на таблицю OrderEntity
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]  // Вказуємо зовнішній ключ
        public OrderEntityModel? Order { get; set; }  // Зв'язок з сутністю OrderEntity

        // Обов'язковий зовнішній ключ на таблицю ProductEntity 
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]  // Вказуємо зовнішній ключ
        public ProductEntityModel? Product { get; set; }  // Зв'язок з сутністю ProductEntity

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
