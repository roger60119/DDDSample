public interface IRedisCacheService
{
    Task<T> GetAsync<T>(string key) where T : class;

    void SetAsync<T>(string key, T value, TimeSpan? expiration = null) where T : class;

    void RemoveAsync(string key);

    bool Exists(string key);
}