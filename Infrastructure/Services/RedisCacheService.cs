
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

internal class RedisCacheService : IRedisCacheService
{
    private readonly IDistributedCache _cache;

    public RedisCacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public bool Exists(string key)
    {
        return _cache.Get(key) != null;
    }

    public T Get<T>(string key) where T : class
    {
        var value = _cache.Get(key) ?? throw new KeyNotFoundException($"Key '{key}' not found in cache.");

        try
        {
            return JsonSerializer.Deserialize<T>(value);
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException($"Failed to deserialize value for key '{key}'.", ex);
        }
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
    }

    public void Set<T>(string key, T value, TimeSpan? expiration = null) where T : class
    {
        _cache.Set(
            key,
            JsonSerializer.SerializeToUtf8Bytes(value),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(60)
            }
        );
    }
}