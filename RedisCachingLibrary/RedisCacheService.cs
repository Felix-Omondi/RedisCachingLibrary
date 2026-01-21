using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace RedisCachingLibrary
{
    /// <summary>
    /// Provides distributed caching functionality using Redis via IDistributedCache.
    /// </summary>
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache redis;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisCacheService"/> class.
        /// </summary>
        /// <param name="redis">The distributed cache implementation.</param>
        public RedisCacheService(IDistributedCache redis)
        {
            this.redis = redis;
        }

        
        /// <inheritdoc/>
        public async Task RemoveAsync<T>(string key)
        {
            await redis.RemoveAsync(key);
        }

        /// <inheritdoc/>
        public async Task<T> GetAsync<T>(string key)
        {
            var data = await redis.GetStringAsync(key);
            if (data is null)
            {
                return default(T);
            }

            return JsonSerializer.Deserialize<T>(data);
        }

        /// <inheritdoc/>
        public async Task SetAsync<T>(string key, T data)
        {
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            };

            await redis.SetStringAsync(key: key, JsonSerializer.Serialize(data), options: options);
        }
    }
}
