namespace Basket.Services.Interfaces
{
    public interface ICacheService
    {
        Task<T> GetAsync<T>(string key);
        Task AddOrUpdateAsync<T>(string key, T value);
        Task RemoveAsync(string key);
    }

}
