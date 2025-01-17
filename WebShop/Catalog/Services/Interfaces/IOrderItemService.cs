using Catalog.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Services
{
    public interface IOrderItemService
    {
        Task<List<OrderItem>> GetAllOrderItemsAsync();
        Task<OrderItem?> GetOrderItemByIdAsync(int id);
        Task AddOrderItemAsync(OrderItem orderItem);
        Task UpdateOrderItemAsync(OrderItem orderItem);
        Task DeleteOrderItemAsync(int id);
    }
}
