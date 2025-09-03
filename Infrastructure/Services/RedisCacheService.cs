
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class RedisCacheService : IRedisCacheService
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<RedisCacheService> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public RedisCacheService(IDistributedCache cache, ILogger<RedisCacheService> logger)
    {
        _cache = cache;
        _logger = logger;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            WriteIndented = false
        };
    }

    public bool Exists(string key)
    {
        return _cache.Get(key) != null;
    }

    public T Get<T>(string key) where T : class
    {
        _logger.LogInformation($"Attempting to retrieve key '{key}' from cache.");

        var value = _cache.Get(key) ?? throw new KeyNotFoundException($"Key '{key}' not found in cache.");

        try
        {
            return JsonSerializer.Deserialize<T>(value, _jsonOptions)!;
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException($"Failed to deserialize value for key '{key}'.", ex);
        }
    }

    public void Remove(string key)
    {
        _logger.LogInformation($"Removing key '{key}' from cache.");

        _cache.Remove(key);
    }

    public void Set<T>(string key, T value, TimeSpan? expiration = null) where T : class
    {
        _logger.LogInformation($"Setting key '{key}' in cache with expiration of {expiration?.TotalMinutes ?? 60} minutes.");

        _cache.Set(
            key,
            JsonSerializer.SerializeToUtf8Bytes(value, _jsonOptions),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(60)
            }
        );
    }
}