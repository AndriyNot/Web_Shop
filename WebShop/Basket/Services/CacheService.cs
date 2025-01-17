using Basket.Services.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Basket.Services
{
    public class CacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly ILogger<CacheService> _logger;

        public CacheService(IConnectionMultiplexer redis, ILogger<CacheService> logger)
        {
            _redis = redis;
            _logger = logger;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var db = _redis.GetDatabase();
            var data = await db.StringGetAsync(key);
            if (data.IsNullOrEmpty)
            {
                _logger.LogWarning($"Cache miss for key: {key}");
                return default;
            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        public async Task AddOrUpdateAsync<T>(string key, T value)
        {
            var db = _redis.GetDatabase();
            var data = JsonConvert.SerializeObject(value);
            await db.StringSetAsync(key, data);
        }

        public async Task RemoveAsync(string key)
        {
            var db = _redis.GetDatabase();
            await db.KeyDeleteAsync(key);
        }
    }

}
