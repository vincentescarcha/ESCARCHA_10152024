using FileProcessor.Data;
using Microsoft.EntityFrameworkCore;

namespace FileProcessor.Services
{
    public class ApiKeyService
    {
        private readonly FileProcessorDbContext _context;

        public ApiKeyService(FileProcessorDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateApiKeyAsync()
        {
            var apiKey = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            var expiration = DateTime.UtcNow.AddHours(1);

            var newApiKey = new ApiKey
            {
                Key = apiKey,
                Expiration = expiration
            };

            _context.ApiKeys.Add(newApiKey);
            await _context.SaveChangesAsync();

            return apiKey;
        }

        public async Task<bool> IsValidApiKeyAsync(string apiKey)
        {
            var apiKeyEntity = await _context.ApiKeys
                .FirstOrDefaultAsync(k => k.Key == apiKey);

            if (apiKeyEntity != null)
            {
                if (DateTime.UtcNow < apiKeyEntity.Expiration)
                {
                    return true;
                }
                else
                {
                    // Remove expired key
                    _context.ApiKeys.Remove(apiKeyEntity);
                    await _context.SaveChangesAsync();
                }
            }
            return false;
        }
    }
}
