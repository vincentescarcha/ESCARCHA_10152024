using FileProcessor.Models;

namespace FileProcessor.Services
{
    public class CsvFileProcessor : IFileProcessor
    {
        public Task<FileProcessingResult> ProcessFileAsync(Stream fileStream, string filter = null)
        {
            throw new NotImplementedException();
        }
    }
}
