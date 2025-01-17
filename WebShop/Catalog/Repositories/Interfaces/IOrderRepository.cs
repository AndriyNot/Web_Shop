using Catalog.Entities;

namespace Catalog.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderEntity>> GetAllOrdersAsync();
        Task<OrderEntity> GetOrderByIdAsync(int id);
        Task AddOrderAsync(OrderEntity order);
        Task UpdateOrderAsync(OrderEntity order);
        Task DeleteOrderAsync(int id);
    }
}
