using FileProcessor.Models;
using FileProcessor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;

namespace FileProcessor.Controllers
{
    [ApiController]
    [Route("api/files")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public class FileProcessingController : ControllerBase
    {
        private readonly ILogger<FileProcessingController> _logger;
        private readonly FileProcessingService _fileProcessingService;

        public FileProcessingController(ILogger<FileProcessingController> logger, FileProcessingService fileProcessingService)
        {
            _logger = logger;
            _fileProcessingService = fileProcessingService;
        }

        /// <summary>
        /// Upload either a CSV or JSON file for processing. 
        /// This automatically detects the file type and routes the request to the appropriate processor.
        /// </summary>
        /// <param name="request">The file to be processed (supports CSV and JSON)</param>
        /// <param name="query">An optional query string that specifies the operation to perform on the uploaded data</param>
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

        /// <summary>
        /// Returns the information about previously processed files including their filenames, sizes, and processing times
        /// </summary>
        [HttpGet("report")]
        [ProducesResponseType(typeof(List<FileInfoDto>), StatusCodes.Status200OK)]
        public IActionResult GetProcessedFilesReport()
        {
            var files = _fileProcessingService.GetProcessedFiles();
            return Ok(files);
        }
    }
}
