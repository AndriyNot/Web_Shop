using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Catalog.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email); // Реалізація отримання користувача по email
        }

        public async Task AddUserAsync(User user)
        {
            await _userRepository.AddUserAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteUserAsync(id);
        }

        // Реєстрація користувача
        public async Task<User?> RegisterAsync(RegistrationModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                throw new Exception("Passwords do not match.");
            }

            var existingUser = await _userRepository.GetAllUsersAsync();
            if (existingUser.Any(u => u.Email == model.Email))
            {
                throw new Exception("User with this email already exists.");
            }

            var passwordHash = HashPassword(model.Password);
            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                PasswordHash = passwordHash
            };

            await _userRepository.AddUserAsync(user);
            return user;
        }

        // Логін користувача
        public async Task<User?> LoginAsync(LoginModel model)
        {
            var user = await _userRepository.GetAllUsersAsync();
            var foundUser = user.FirstOrDefault(u => u.Email == model.Email);

            if (foundUser == null || !VerifyPassword(model.Password, foundUser.PasswordHash))
            {
                throw new Exception("Invalid credentials.");
            }

            return foundUser;
        }

        // Хешування паролю
        private string HashPassword(string password)
        {
            var salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hash;
        }

        // Перевірка паролю
        private bool VerifyPassword(string password, string storedHash)
        {
            return storedHash == HashPassword(password);
        }
    }
}
