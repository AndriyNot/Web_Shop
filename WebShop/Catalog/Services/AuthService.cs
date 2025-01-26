using Catalog.Entities;
using Catalog.Models;
using Catalog.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        // Отримуємо налаштування JWT через властивості
        private string JwtSecret => _configuration["Jwt:Key"];
        private string JwtIssuer => _configuration["Jwt:Issuer"];
        private string JwtAudience => _configuration["Jwt:Audience"];

        // Реєстрація користувача
        public async Task<AuthResponseDto> RegisterAsync(RegistrationModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                throw new Exception("Паролі не співпадають.");
            }

            var existingUser = await _userRepository.GetUserByEmailAsync(model.Email);
            if (existingUser != null)
            {
                throw new Exception("Користувач з таким email вже існує.");
            }

            if (!IsStrongPassword(model.Password))
            {
                throw new Exception("Пароль повинен бути не менше 8 символів і містити букви та цифри.");
            }

            var passwordHash = HashPassword(model.Password, out string salt);
            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                PasswordHash = $"{salt}:{passwordHash}" // Зберігаємо сіль і хеш разом
            };

            await _userRepository.AddUserAsync(user);

            // Генерація токену після реєстрації
            var token = GenerateJwtToken(user);
            return new AuthResponseDto { Token = token };
        }

        // Логін користувача
        public async Task<AuthResponseDto> LoginAsync(LoginModel model)
        {
            var user = await _userRepository.GetUserByEmailAsync(model.Email);

            if (user == null || !VerifyPassword(model.Password, user.PasswordHash))
            {
                throw new Exception("Невірні дані для входу.");
            }

            // Генерація токену після логіну
            var token = GenerateJwtToken(user);
            return new AuthResponseDto { Token = token };
        }

        // Хешування паролю
        public string HashPassword(string password, out string salt)
        {
            // Генерація випадкової солі
            var saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }

            salt = Convert.ToBase64String(saltBytes);

            // Хешування пароля з сіллю
            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hash;
        }

        // Перевірка пароля
        private bool VerifyPassword(string password, string storedHash)
        {
            // Розділяємо хеш і сіль
            var parts = storedHash.Split(':');
            if (parts.Length != 2)
            {
                throw new FormatException("Збережений хеш має неправильний формат.");
            }

            var salt = Convert.FromBase64String(parts[0]);
            var hash = parts[1];

            // Генеруємо хеш для введеного пароля з використанням збереженої солі
            var computedHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return computedHash == hash;
        }

        // Генерація JWT токену
        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: JwtIssuer,
                audience: JwtAudience,
                claims: claims,
                expires: DateTime.Now.AddHours(12), // Термін дії токену 12 годин
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Перевірка на сильний пароль
        private bool IsStrongPassword(string password)
        {
            return password.Length >= 8 && password.Any(char.IsDigit) && password.Any(char.IsLetter);
        }

    }
}
