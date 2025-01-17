namespace Basket.Models
{
    public class BasketDto
    {
        public string UserId { get; set; }
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    }
}