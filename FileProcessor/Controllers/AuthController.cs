using FileProcessor.Models;
using FileProcessor.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileProcessor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApiKeyService _apiKeyService;

        public AuthController(ApiKeyService apiKeyService)
        {
            _apiKeyService = apiKeyService;
        }

        /// <summary>
        /// Generates a new API key with expiration.
        /// </summary>
        /// <response code="200">Returns the newly generated API key and expiration date</response>
        /// <response code="500">If the application fail</response>
        [HttpPost("generate-api-key")]
        [ProducesResponseType(typeof(ApiKeyResponse), StatusCodes.Status200OK)]
        public IActionResult GenerateApiKey()
        {
            var apiKey = _apiKeyService.GenerateApiKey();
            var response = new ApiKeyResponse
            {
                ApiKey = apiKey,
                Expiration = DateTime.UtcNow.AddHours(1)
            };
            return Ok(response);
        }

    }
}
