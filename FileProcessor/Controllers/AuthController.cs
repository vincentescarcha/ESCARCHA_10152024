using FileProcessor.Models;
using FileProcessor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FileProcessor.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ApiKeyService _apiKeyService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ApiKeyService apiKeyService, ILogger<AuthController> logger)
        {
            _apiKeyService = apiKeyService;
            _logger = logger;
        }

        /// <summary>
        /// Generates a new API key with expiration.
        /// </summary>
        /// <response code="200">Returns the newly generated API key and expiration date</response>
        /// <response code="500">If an error occurs</response>
        [HttpPost("generate-api-key")]
        [ProducesResponseType(typeof(ApiKeyResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GenerateApiKey()
        {
            try
            {
                var apiKey = await _apiKeyService.GenerateApiKeyAsync();
                var response = new ApiKeyResponse
                {
                    ApiKey = apiKey,
                    Expiration = DateTime.UtcNow.AddHours(1)
                };
                return Ok(response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.Messages.ApiKeyGenerationError);

                var errorResponse = new Error
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = Constants.Messages.ApiKeyGenerationError,
                    Details = ex.Message 
                };
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }
    }
}
