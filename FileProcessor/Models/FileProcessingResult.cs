namespace FileProcessor.Models
{
    public class FileProcessingResult
    {
        public string Result { get; set; }
        public List<string> Errors { get; set; } = new();
        public List<string> Warnings { get; set; } = new();
        public List<string> Message { get; set; } = new();
    }
}
