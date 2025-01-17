using Catalog.Entities;
using Catalog.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Catalog.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        // Отримання всіх брендів
        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await _brandService.GetAllBrandsAsync();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,  // Підтримка циклічних посилань
                WriteIndented = true,  // Форматування JSON для зручності
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull // Ігноруємо null значення
            };

            var jsonResponse = JsonSerializer.Serialize(brands, options);

            return Content(jsonResponse, "application/json");
        }

        // Отримання бренду за ідентифікатором
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrandById(int id)
        {
            var brand = await _brandService.GetBrandByIdAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,  // Підтримка циклічних посилань
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull // Ігноруємо null значення
            };

            var jsonResponse = JsonSerializer.Serialize(brand, options);

            return Content(jsonResponse, "application/json");
        }

        // Додавання нового бренду
        [HttpPost]
        public async Task<IActionResult> AddBrand([FromBody] Brand brand)
        {
            if (brand == null)
            {
                return BadRequest("Brand cannot be null.");
            }

            await _brandService.AddBrandAsync(brand);
            return CreatedAtAction(nameof(GetBrandById), new { id = brand.Id }, brand);
        }

        // Оновлення бренду
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(int id, [FromBody] Brand brand)
        {
            if (id != brand.Id)
            {
                return BadRequest("Brand ID mismatch.");
            }

            var existingBrand = await _brandService.GetBrandByIdAsync(id);
            if (existingBrand == null)
            {
                return NotFound();
            }

            await _brandService.UpdateBrandAsync(brand);
            return NoContent();
        }

        // Видалення бренду
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _brandService.GetBrandByIdAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            await _brandService.DeleteBrandAsync(id);
            return NoContent();
        }
    }
}
