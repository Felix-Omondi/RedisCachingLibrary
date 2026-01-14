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

        /// <summary>
        /// Removes an item from the cache by key.
        /// </summary>
        /// <typeparam name="T">The expected type (unused in removal).</typeparam>
        /// <param name="key">The cache key to remove.</param>
        public async Task DeleteDataAsync<T>(string key)
        {
            await redis.RemoveAsync(key);
        }


        /// <summary>
        /// Retrieves a cached item of type T by key.
        /// </summary>
        /// <typeparam name="T">The type of the cached object.</typeparam>
        /// <param name="key">The cache key.</param>
        /// <returns>The deserialized object, or default(T) if not found.</returns>
        public async Task<T> GetDataAsync<T>(string key)
        {
            var data = await redis.GetStringAsync(key);
            if (data is null)
            {
                return default(T);
            }

            return JsonSerializer.Deserialize<T>(data);
        }

        /// <summary>
        /// Stores an object in the cache with a 5-minute sliding expiration.
        /// </summary>
        /// <typeparam name="T">The type of the object to cache.</typeparam>
        /// <param name="key">The cache key.</param>
        /// <param name="data">The object to cache</param>
        /// <returns></returns>
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
