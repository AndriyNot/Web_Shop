using Catalog.Services;
using Catalog.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Catalog.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // Реєстрація користувача
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationModel registrationModel)
        {
            
                var authResponse = await _authService.RegisterAsync(registrationModel);
                return Ok(authResponse);
            
        }

        // Логін користувача
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                var authResponse = await _authService.LoginAsync(loginModel);
                return Ok(authResponse);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        // Вихід
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Вихід без зберігання сесії (для JWT)
            return Ok("Ви вийшли з системи");
        }
    }
}
