using System.Security.Cryptography;
using System.Text.Json;
using DDDSample.Infrastructure.Common;

namespace DDDSample.Infrastructure.Services
{
    public class EntityChangeDetectorService : IEntityChangeDetectorService
    {
        private readonly ILogger<EntityChangeDetectorService> _logger;
        private readonly IHostEnvironment _hostEnvironment;
        private const string HashFileName = "entity_hashes.json";

        public EntityChangeDetectorService(ILogger<EntityChangeDetectorService> logger, IHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        public async Task ExecuteAsync()
        {
            _logger.LogInformation("Starting entity change detection job.");

            var domainPath = Path.Combine(_hostEnvironment.ContentRootPath, "DDDSample/Domain");
            var entityFiles = Directory.GetFiles(domainPath, "*.cs", SearchOption.AllDirectories)
                                       .Where(f => f.Contains(Path.Combine("Entities")));

            var currentHashes = new Dictionary<string, string>();
            foreach (var file in entityFiles)
            {
                var hash = await ComputeFileHashAsync(file);
                currentHashes[file] = hash;
            }

            var previousHashes = await ReadPreviousHashesAsync();

            if (HashesChanged(previousHashes, currentHashes))
            {
                _logger.LogWarning("Entity model change detected. Please review your DbContext and create a new migration if necessary.");
            }
            else
            {
                _logger.LogInformation("No changes detected in entity models.");
            }

            await WriteHashesAsync(currentHashes);
            _logger.LogInformation("Finished entity change detection job.");
        }

        private async Task<string> ComputeFileHashAsync(string filePath)
        {
            using (var sha256 = SHA256.Create())
            using (var stream = File.OpenRead(filePath))
            {
                var hashBytes = await sha256.ComputeHashAsync(stream);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }

        private string GetHashFilePath()
        {
            return Path.Combine(_hostEnvironment.ContentRootPath, HashFileName);
        }

        private async Task<Dictionary<string, string>> ReadPreviousHashesAsync()
        {
            var hashFilePath = GetHashFilePath();
            if (!File.Exists(hashFilePath))
            {
                return new Dictionary<string, string>();
            }

            try
            {
                var json = await File.ReadAllTextAsync(hashFilePath);
                return JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading previous entity hashes file.");
                return new Dictionary<string, string>();
            }
        }

        private async Task WriteHashesAsync(Dictionary<string, string> hashes)
        {
            var hashFilePath = GetHashFilePath();
            try
            {
                var json = JsonSerializer.Serialize(hashes, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(hashFilePath, json);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error writing entity hashes file.");
            }
        }

        private bool HashesChanged(Dictionary<string, string> previous, Dictionary<string, string> current)
        {
            if (previous.Count != current.Count)
            {
                return true;
            }

            foreach (var item in current)
            {
                if (!previous.TryGetValue(item.Key, out var prevHash) || prevHash != item.Value)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
