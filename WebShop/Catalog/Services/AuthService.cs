using Catalog.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(IUserService userService, IConfiguration configuration, IPasswordHasher<User> passwordHasher)
        {
            _userService = userService;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        // Реєстрація користувача
        public async Task<string> RegisterAsync(RegistrationModel model)
        {
            // Перевірка, чи існує користувач
            var existingUser = await _userService.GetUserByEmailAsync(model.Email);
            if (existingUser != null)
            {
                throw new Exception("User with this email already exists.");
            }

            // Перевірка чи паролі збігаються
            if (model.Password != model.ConfirmPassword)
            {
                throw new Exception("Passwords do not match.");
            }

            // Хешування паролю
            var hashedPassword = _passwordHasher.HashPassword(null, model.Password);

            // Створення нового користувача
            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                Password = hashedPassword
            };

            await _userService.AddUserAsync(user);

            // Повернення JWT токену після реєстрації
            return GenerateJwtToken(user);
        }

        // Логін користувача
        public async Task<string> LoginAsync(LoginModel model)
        {
            // Пошук користувача за імейлом
            var user = await _userService.GetUserByEmailAsync(model.Email);
            if (user == null || _passwordHasher.VerifyHashedPassword(user, user.Password, model.Password) != PasswordVerificationResult.Success)
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            // Повернення JWT токену після успішного логіну
            return GenerateJwtToken(user);
        }

        // Лог-аут (можна реалізувати видалення токену або закриття сесії)
        public Task LogoutAsync()
        {
            // Для JWT не потрібно зберігати токен на сервері, можна просто видалити його на клієнті.
            return Task.CompletedTask;
        }

        // Генерація JWT токену
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, user.Name),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
