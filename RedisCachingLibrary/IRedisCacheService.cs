namespace RedisCachingLibrary
{
    /// <summary>
    /// Interface for Redis-based distributed caching operations.
    /// </summary>
    public interface IRedisCacheService
    {
        /// <summary>
        /// Gets a cached item by key.
        /// </summary>
        Task<T> GetDataAsync<T>(string key);

        /// <summary>
        /// Sets a cached item with 30-minute expiration.
        /// </summary>
        Task SetDataAsync<T>(string key, T data);

        /// <summary>
        /// Removed a cached item by key.
        /// </summary>
        Task DeleteDataAsync<T>(string key);
    }
}
