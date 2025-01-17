using Catalog.Entities;
using Catalog.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Catalog.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // Отримання всіх категорій
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,  // Підтримка циклічних посилань
                WriteIndented = true,  // Форматування JSON для зручності
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull // Ігноруємо властивості, які дорівнюють null
            };

            var jsonResponse = JsonSerializer.Serialize(categories, options);  // Серіалізація з налаштуваннями

            return Content(jsonResponse, "application/json");
        }

        // Отримання категорії за ідентифікатором
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,  // Підтримка циклічних посилань
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull // Ігноруємо властивості, які дорівнюють null
            };

            var jsonResponse = JsonSerializer.Serialize(category, options);

            return Content(jsonResponse, "application/json");
        }

        // Додавання нової категорії
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            await _categoryService.AddCategoryAsync(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }

        // Оновлення категорії
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            await _categoryService.UpdateCategoryAsync(category);
            return NoContent();
        }

        // Видалення категорії
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
