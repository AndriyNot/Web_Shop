using Catalog.Entities;

namespace Catalog.Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductEntity>> GetAllProductsAsync();
        Task<ProductEntity> GetProductByIdAsync(int id);
        Task AddProductAsync(ProductEntity product);
        Task UpdateProductAsync(ProductEntity product);
        Task DeleteProductAsync(int id);
    }

}
