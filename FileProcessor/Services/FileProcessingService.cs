using FileProcessor.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.IO;
using System.IO.Pipes;
using static System.Net.WebRequestMethods;

namespace FileProcessor.Services
{
    public class FileProcessingService
    {
        // Store uploaded files in-memory
        private readonly ConcurrentDictionary<string, byte[]> _files = new();
        private readonly ILogger<FileProcessingService> _logger;
        private readonly IServiceScopeFactory _serviceProvider;

        public FileProcessingService(ILogger<FileProcessingService> logger, IServiceScopeFactory serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task<FileProcessingResult> ProcessFileAsync(IFormFile file, string query)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException();
            }

            string fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant().TrimStart('.');
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            _files[file.FileName] = memoryStream.ToArray();


            IFileProcessor processor;
            using var scope = _serviceProvider.CreateScope();

            if (fileExtension == "csv")
            {
                processor = scope.ServiceProvider.GetRequiredService<CsvFileProcessor>();
            }
            else if (fileExtension == "json")
            {
                processor = scope.ServiceProvider.GetRequiredService<JsonFileProcessor>();
            }
            else
            {
                throw new InvalidOperationException(Constants.Messages.UnsupportedFileType);
            }

            using var fileStream = file.OpenReadStream();
            var result = await processor.ProcessFileAsync(fileStream, query);

            return result;
        }
    }
}
