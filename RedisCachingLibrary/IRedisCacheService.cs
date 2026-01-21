namespace RedisCachingLibrary
{
    /// <summary>
    /// Interface for Redis-based distributed caching operations.
    /// </summary>
    public interface IRedisCacheService
    {
        /// <summary>
        /// Retrieves a cached item of type T by key.
        /// </summary>
        /// <typeparam name="T">The type of the cached object.</typeparam>
        /// <param name="key">The cache key.</param>
        /// <returns>The deserialized object, or default(T) if not found.</returns>
        Task<T> GetAsync<T>(string key);

        /// <summary>
        /// Stores an object in the cache with a 5-minute sliding expiration.
        /// </summary>
        /// <typeparam name="T">The type of the object to cache.</typeparam>
        /// <param name="key">The cache key.</param>
        /// <param name="data">The object to cache</param>
        Task SetAsync<T>(string key, T data);

        /// <summary>
        /// Removes an item from the cache by key.
        /// </summary>
        /// <typeparam name="T">The expected type (unused in removal).</typeparam>
        /// <param name="key">The cache key to remove.</param>
        Task RemoveAsync<T>(string key);
    }
}
