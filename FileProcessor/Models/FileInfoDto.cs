namespace FileProcessor.Models
{
    public class FileInfoDto
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public TimeSpan ProcessingTime { get; set; }
    }
}
