using Catalog.Entities;
using Catalog.Repositories;

namespace Catalog.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly IProductRepository _productRepository;

        public UserService(IUserRepository userRepository, IAuthService authService, IProductRepository productRepository)
        {
            _userRepository = userRepository;
            _authService = authService;
            _productRepository = productRepository;
        }

        public async Task AddUserAsync(User user, string password)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
            if (existingUser != null)
            {
                throw new Exception("Користувач з таким email вже існує.");
            }

            // Використовуємо метод з AuthService для хешування пароля
            var passwordHash = _authService.HashPassword(password, out string salt);
            user.PasswordHash = $"{salt}:{passwordHash}";

            await _userRepository.AddUserAsync(user);
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteUserAsync(id);
        }

        // Додавання товару в список покупок (BuyList)
        public async Task AddToBuyListAsync(int userId, int productId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                throw new Exception("Користувача не знайдено.");

            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null)
                throw new Exception("Товар не знайдений.");

            if (user.BuyList == null)
            {
                user.BuyList = new List<ProductEntity>();
            }

            // Перевірка, чи товар вже є в списку покупок
            if (user.BuyList.Any(p => p.Id == productId))
                throw new Exception("Товар вже є в списку покупок.");

            // Перетворення продукту на відповідну сутність для списку покупок
            user.BuyList.Add(product);

            await _userRepository.SaveChangesAsync();
        }

        // Додавання товару в список бажаних товарів (WishList)
        public async Task AddToWishListAsync(int userId, int productId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                throw new Exception("Користувача не знайдено.");

            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null)
                throw new Exception("Товар не знайдено.");

            if (user.WishList == null)
            {
                user.WishList = new List<ProductEntity>();
            }

            // Перевірка, чи товар вже є в списку бажаних товарів
            if (user.WishList.Any(p => p.Id == productId))
                throw new Exception("Товар вже є в списку бажаних товарів.");

            // Перетворення продукту на відповідну сутність для списку бажаних товарів
            user.WishList.Add(product);

            await _userRepository.SaveChangesAsync();
        }

        // Отримання списку покупок
        public async Task<List<ProductEntity>> GetBuyListAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                throw new Exception("Користувача не знайдено.");

            return user.BuyList ?? new List<ProductEntity>();
        }

        // Отримання списку бажаних товарів
        public async Task<List<ProductEntity>> GetWishListAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                throw new Exception("Користувача не знайдено.");

            return user.WishList ?? new List<ProductEntity>();
        }
    }
}
