using Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Catalog.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Отримання всіх замовлень
        public async Task<IEnumerable<OrderEntity>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderItems) // Завантажуємо елементи замовлення
                .ToListAsync();
        }

        // Отримання замовлення за ID
        public async Task<OrderEntity> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        // Додавання нового замовлення
        public async Task AddOrderAsync(OrderEntity order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        // Оновлення замовлення
        public async Task UpdateOrderAsync(OrderEntity order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        // Видалення замовлення
        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
