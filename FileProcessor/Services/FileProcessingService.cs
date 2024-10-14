using FileProcessor.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using static System.Net.WebRequestMethods;

namespace FileProcessor.Services
{
    public class FileProcessingService
    {
        // Store uploaded files in-memory
        private readonly ConcurrentDictionary<string, FileInfoDto> _files = new();
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
                throw new ArgumentException(Constants.Messages.NoFileUploaded);
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            string fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant().TrimStart('.');

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
            var size = await GetFileSize(file);

            stopwatch.Stop();

            _files[file.FileName] = new FileInfoDto
            {
                Name = file.FileName,
                Size = size,
                ProcessingTime = stopwatch.Elapsed
            };

            return result;
        }

        private static async Task<string> GetFileSize(IFormFile file)
        {
            var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var fileSize = memoryStream.ToArray().Length;
            string sizeFormatted;
            if (fileSize >= 1024 * 1024) 
            {
                sizeFormatted = $"{fileSize / (1024 * 1024)} MB";
            }
            else if (fileSize >= 1024) 
            {
                sizeFormatted = $"{fileSize / 1024} KB";
            }
            else 
            {
                sizeFormatted = $"{fileSize} B"; 
            }


            return sizeFormatted;
        }

        public List<FileInfoDto> GetProcessedFiles()
        {
            return _files.Select(x => new FileInfoDto
            {
                Name = x.Value.Name,
                Size = x.Value.Size,
                ProcessingTime = x.Value.ProcessingTime
            }).ToList();
        }

    }
}
