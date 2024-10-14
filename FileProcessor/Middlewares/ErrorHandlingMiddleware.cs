using FileProcessor.Models;
using Newtonsoft.Json;

namespace FileProcessor.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Just pass control to the next middleware but sets a try catch
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.Messages.UnexpectedError);

                // Handle the exception
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorResponse = new Error
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = Constants.Messages.UnexpectedError,
                Details = exception.Message 
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var result = JsonConvert.SerializeObject(errorResponse);
            return context.Response.WriteAsync(result);
        }

    }
}
