using Catalog.Entities;

namespace Catalog.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<User?> RegisterAsync(RegistrationModel model);
        Task<User?> LoginAsync(LoginModel model);
        Task<User> GetUserByEmailAsync(string email);
    }
}
