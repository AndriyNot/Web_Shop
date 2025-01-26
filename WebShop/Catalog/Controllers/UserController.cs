using Catalog.Entities;
using Catalog.Models;
using Catalog.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Отримати користувача за ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound($"Користувача з ID {id} не знайдено.");
            }
            return Ok(user);
        }

        // Створити нового користувача
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] RegistrationModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest("Невірні дані користувача.");
            }

            var user = new User
            {
                Name = model.Name,
                Email = model.Email
                // Тут інші поля, якщо потрібно
            };

            // Передаємо пароль разом з користувачем для хешування
            await _userService.AddUserAsync(user, model.Password);

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        // Оновити користувача
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (user == null || id != user.Id)
            {
                return BadRequest("Невірні дані користувача.");
            }

            var existingUser = await _userService.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound($"Користувача з ID {id} не знайдено.");
            }

            await _userService.UpdateUserAsync(user);
            return NoContent();
        }

        // Видалити користувача
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound($"Користувача з ID {id} не знайдено.");
            }

            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

        // Додати товар в список покупок
        [HttpPost("{userId}/{productId}")]
        public async Task<IActionResult> AddToBuyList(int userId, int productId)
        {
            try
            {
                await _userService.AddToBuyListAsync(userId, productId);
                return Ok("Товар успішно додано до списку покупок.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Додати товар в список бажаних товарів
        [HttpPost("{userId}/{productId}")]
        public async Task<IActionResult> AddToWishList(int userId, int productId)
        {
            try
            {
                await _userService.AddToWishListAsync(userId, productId);
                return Ok("Товар успішно додано до списку бажаних товарів.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Отримати список покупок
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetBuyList(int userId)
        {
            try
            {
                var buyList = await _userService.GetBuyListAsync(userId);
                return Ok(buyList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Отримати список бажаних товарів
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetWishList(int userId)
        {
            try
            {
                var wishList = await _userService.GetWishListAsync(userId);
                return Ok(wishList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
