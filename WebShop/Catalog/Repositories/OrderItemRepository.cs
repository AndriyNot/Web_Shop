using Catalog.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Завантаження всіх OrderItems разом з відповідними Order і Product
        public async Task<List<OrderItem>> GetAllOrderItemsAsync()
        {
            return await _context.OrderItems
                                 .Include(oi => oi.Order)  // Завантажуємо Order для кожного OrderItem
                                 .Include(oi => oi.Product) // Завантажуємо Product для кожного OrderItem
                                     .ThenInclude(p => p.Category) // Завантажуємо категорію для кожного продукту
                                 .Include(oi => oi.Product.Brand) // Завантажуємо бренд для кожного продукту
                                 .ToListAsync();
        }

        // Завантаження OrderItem за ID
        public async Task<OrderItem?> GetOrderItemByIdAsync(int id)
        {
            return await _context.OrderItems
                                 .Include(oi => oi.Order)
                                 .Include(oi => oi.Product)
                                     .ThenInclude(p => p.Category)
                                 .Include(oi => oi.Product.Brand)
                                 .FirstOrDefaultAsync(oi => oi.Id == id);
        }

        // Додавання нового OrderItem
        public async Task AddOrderItemAsync(OrderItem orderItem)
        {
            if (orderItem == null) throw new ArgumentNullException(nameof(orderItem));

            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();
        }

        // Оновлення OrderItem
        public async Task UpdateOrderItemAsync(OrderItem orderItem)
        {
            if (orderItem == null) throw new ArgumentNullException(nameof(orderItem));

            var existingOrderItem = await _context.OrderItems.FindAsync(orderItem.Id);
            if (existingOrderItem == null)
            {
                throw new KeyNotFoundException("OrderItem not found");
            }

            existingOrderItem.Quantity = orderItem.Quantity;
            existingOrderItem.Price = orderItem.Price;

            await _context.SaveChangesAsync();
        }

        // Видалення OrderItem
        public async Task DeleteOrderItemAsync(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                throw new KeyNotFoundException("OrderItem not found");
            }

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
        }
    }
}
