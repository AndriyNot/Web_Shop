using Basket.Models;

namespace Basket.Services
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasket(string userId);
        Task<BasketDto> AddItemsToBasket(string userId, List<BasketItemDto> items);
        Task RemoveItem(string userId, int catalogItemId);
        Task ClearBasket(string userId);
    }
}