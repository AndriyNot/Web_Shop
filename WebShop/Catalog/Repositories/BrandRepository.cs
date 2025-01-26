using Catalog.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _context;

        public BrandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Завантаження всіх брендів з продуктами
        public async Task<List<Brand>> GetAllBrandsAsync()
        {
            return await _context.Brands
                                 .Include(b => b.Products)  // Завантажуємо продукти, що належать бренду
                                 .ToListAsync();
        }

        // Завантаження бренду за ідентифікатором
        public async Task<Brand?> GetBrandByIdAsync(int id)
        {
            return await _context.Brands
                                 .Include(b => b.Products)  // Завантажуємо продукти разом з брендом
                                 .FirstOrDefaultAsync(b => b.Id == id);
        }

        // Додавання нового бренду
        public async Task AddBrandAsync(Brand brand)
        {
            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();
        }

        // Оновлення бренду
        public async Task UpdateBrandAsync(Brand brand)
        {
            _context.Brands.Update(brand);
            await _context.SaveChangesAsync();
        }

        // Видалення бренду
        public async Task DeleteBrandAsync(int id)
        {
            var brand = await GetBrandByIdAsync(id);
            if (brand != null)
            {
                _context.Brands.Remove(brand);
                await _context.SaveChangesAsync();
            }
        }
    }
}
