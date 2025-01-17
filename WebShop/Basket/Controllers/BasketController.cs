using Basket.Models;
using Basket.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

[ApiController]
[Route("api/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketService _basketService;

    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetBasket(string userId)
    {
        var basket = await _basketService.GetBasket(userId);
        return Ok(basket);
    }

    [HttpPost("{userId}/add")]
    public async Task<IActionResult> AddItemsToBasket(string userId, [FromBody] List<BasketItemDto> items)
    {
        var updatedBasket = await _basketService.AddItemsToBasket(userId, items);
        return Ok(updatedBasket);
    }

    [HttpDelete("{userId}/remove/{catalogItemId}")]
    public async Task<IActionResult> RemoveItem(string userId, int catalogItemId)
    {
        await _basketService.RemoveItem(userId, catalogItemId);
        return NoContent();
    }

    [HttpDelete("{userId}/clear")]
    public async Task<IActionResult> ClearBasket(string userId)
    {
        await _basketService.ClearBasket(userId);
        return NoContent();
    }
}
