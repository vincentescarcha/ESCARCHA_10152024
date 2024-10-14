using FileProcessor.Models;

namespace FileProcessor.Services
{
    public interface IFileProcessor
    {
        Task<FileProcessingResult> ProcessFileAsync(Stream fileStream, string filter);
    }
}
