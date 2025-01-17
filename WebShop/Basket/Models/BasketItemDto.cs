namespace Basket.Models
{
    public class BasketItemDto
    {
        public int CatalogItemId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

}