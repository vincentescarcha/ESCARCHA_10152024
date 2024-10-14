using FileProcessor.Models;
using FileProcessor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;

namespace FileProcessor.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FileProcessingController : ControllerBase
    {
        private readonly ILogger<FileProcessingController> _logger;
        private readonly FileProcessingService _fileProcessingService;

        public FileProcessingController(ILogger<FileProcessingController> logger, FileProcessingService fileProcessingService)
        {
            _logger = logger;
            _fileProcessingService = fileProcessingService;
        }

        [HttpPost("upload")]
        [ProducesResponseType(typeof(FileProcessingResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UploadFile([FromForm]FileProcessingRequest request, [FromQuery] string query = null)
        {
            try
            {
                var result = await _fileProcessingService.ProcessFileAsync(request.File, query);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, Constants.Messages.NoFileUploaded);
                var errorResponse = new Error
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = Constants.Messages.NoFileUploaded,
                    Details = ex.Message
                };
                return BadRequest(errorResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.Messages.FileUploadedError);
                var errorResponse = new Error
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = Constants.Messages.FileUploadedError,
                    Details = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }
    }
}
