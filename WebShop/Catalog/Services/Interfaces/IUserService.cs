using Catalog.Entities;

namespace Catalog.Services
{
    public interface IUserService
    {
        Task<User?> GetUserByIdAsync(int id);
        Task AddUserAsync(User user, string password);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
        Task<List<ProductEntity>> GetWishListAsync(int userId);
        Task<List<ProductEntity>> GetBuyListAsync(int userId);
        Task AddToWishListAsync(int userId, int productId);
        Task AddToBuyListAsync(int userId, int productId);
    }
}
