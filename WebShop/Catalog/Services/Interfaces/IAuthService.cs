using Catalog.Entities;
using System.Threading.Tasks;

namespace Catalog.Services
{
    public interface IAuthService
    {
        // Метод реєстрації користувача
        Task<string> RegisterAsync(RegistrationModel model);

        // Метод логіну користувача
        Task<string> LoginAsync(LoginModel model);

        // Метод лог-ауту користувача
        Task LogoutAsync();
    }
}
