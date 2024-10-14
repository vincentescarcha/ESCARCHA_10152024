namespace FileProcessor.Services
{
    public class ApiKeyService
    {
        // Store api keys in-memory instead of database
        private readonly Dictionary<string, DateTime> _apiKeys = new();

        public string GenerateApiKey()
        {
            var apiKey = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            // Set expiration for 1 hour
            _apiKeys[apiKey] = DateTime.UtcNow.AddHours(1); 
            return apiKey;
        }

        public bool IsValidApiKey(string apiKey)
        {
            var isValid = false;
            if (_apiKeys.TryGetValue(apiKey, out var expiration))
            {
                if (DateTime.UtcNow < expiration)
                {
                    isValid = true; 
                }
                else
                {
                    // Remove expired key
                    _apiKeys.Remove(apiKey); 
                }
            }
            return isValid; 
        }

    }
}
