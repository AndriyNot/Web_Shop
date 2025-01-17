using Catalog.Entities;

namespace Catalog.Services
{
    public interface IProductService
    {
        Task<List<ProductEntity>> GetAllProductsAsync();
        Task<ProductEntity> GetProductByIdAsync(int id);
        Task AddProductAsync(ProductEntity product);
        Task UpdateProductAsync(ProductEntity product);
        Task DeleteProductAsync(int id);
    }

}
