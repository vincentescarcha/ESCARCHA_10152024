using FileProcessor.Services;

namespace FileProcessor.Middlewares
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly List<string> _protectedUrls = new List<string>
        {
            "/api/files",
        };

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            if (_protectedUrls.Any(url => context.Request.Path.StartsWithSegments(url)))
            {
                // Check for API Key in the request header
                if (!context.Request.Headers.TryGetValue("x-api-key", out var extractedApiKey))
                {
                    context.Response.StatusCode = 401; // Unauthorized
                    await context.Response.WriteAsync(Constants.Messages.NoApiKeyProvided);
                    return;
                }

                var apiKeyService = scope.ServiceProvider.GetRequiredService<ApiKeyService>();

                var isValid = await apiKeyService.IsValidApiKeyAsync(extractedApiKey);

                if (!isValid)
                {
                    context.Response.StatusCode = 403; // Forbidden
                    await context.Response.WriteAsync(Constants.Messages.InvalidApiKey);
                    return;
                }
            }
            await _next(context);
        }
    }


}
