using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace RedisCachingLibrary
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache redis;

        public RedisCacheService(IDistributedCache redis)
        {
            this.redis = redis;
        }

        public async Task DeleteDataAsync<T>(string key)
        {
            await redis.RemoveAsync(key);
        }

        public async Task<T> GetDataAsync<T>(string key)
        {
            var data = await redis.GetStringAsync(key);
            if (data is null)
            {
                return default(T);
            }

            return JsonSerializer.Deserialize<T>(data);
        }

        public async Task SetDataAsync<T>(string key, T data)
        {
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };

            await redis.SetStringAsync(key: key, JsonSerializer.Serialize(data), options: options);
        }
    }
}
