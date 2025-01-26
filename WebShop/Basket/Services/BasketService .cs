using Basket.Models;
using Basket.Services.Interfaces;
using Basket.Services;

public class BasketService : IBasketService
{
    private readonly ICacheService _cacheService;
    private readonly ILogger<BasketService> _logger;

    public BasketService(ICacheService cacheService, ILogger<BasketService> logger)
    {
        _cacheService = cacheService;
        _logger = logger;
    }

    public async Task<BasketDto> GetBasket(string userId)
    {
        _logger.LogInformation($"Attempting to get basket for user ID: {userId}");
        var basket = await _cacheService.GetAsync<BasketDto>(userId);
        if (basket == null)
        {
            _logger.LogInformation($"Basket not found for user ID: {userId}. Creating new basket.");
            basket = new BasketDto { UserId = userId, Items = new List<BasketItemDto>() };
            await _cacheService.AddOrUpdateAsync(userId, basket);
        }
        else
        {
            _logger.LogInformation($"Basket found for user ID: {userId}");
        }

        return basket;
    }

    public async Task<BasketDto> AddItemsToBasket(string userId, List<BasketItemDto> items)
    {
        _logger.LogInformation($"Attempting to add items to basket for user ID: {userId}");
        var basket = await GetBasket(userId);

        foreach (var item in items)
        {
            var existingItem = basket.Items.FirstOrDefault(i => i.CatalogItemId == item.CatalogItemId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                basket.Items.Add(item);
            }
        }

        await _cacheService.AddOrUpdateAsync(userId, basket);
        _logger.LogInformation($"Items added to basket for user ID: {userId}");
        return basket;
    }

    public async Task RemoveItem(string userId, int catalogItemId)
    {
        _logger.LogInformation($"Attempting to remove item from basket for user ID: {userId}");
        var basket = await GetBasket(userId);
        if (basket != null)
        {
            basket.Items.RemoveAll(item => item.CatalogItemId == catalogItemId);
            await _cacheService.AddOrUpdateAsync(userId, basket);
            _logger.LogInformation($"Item removed from basket for user ID: {userId}");
        }
    }

    public async Task ClearBasket(string userId)
    {
        _logger.LogInformation($"Attempting to clear basket for user ID: {userId}");
        await _cacheService.AddOrUpdateAsync(userId, new BasketDto { UserId = userId, Items = new List<BasketItemDto>() });
        _logger.LogInformation($"Basket cleared for user ID: {userId}");
    }
}
