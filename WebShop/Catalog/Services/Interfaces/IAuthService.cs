using Catalog.Entities;
using Catalog.Models;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegistrationModel model);
    Task<AuthResponseDto> LoginAsync(LoginModel model);
    string HashPassword(string password, out string salt);
}


