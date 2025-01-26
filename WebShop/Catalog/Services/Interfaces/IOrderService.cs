using Catalog.Entities;

namespace Catalog.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderEntity>> GetAllOrdersAsync();
        Task<OrderEntity> GetOrderByIdAsync(int id);
        Task AddOrderAsync(OrderEntity order);
        Task UpdateOrderAsync(OrderEntity order);
        Task DeleteOrderAsync(int id);
    }
}
