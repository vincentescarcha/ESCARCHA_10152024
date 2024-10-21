using System.ComponentModel.DataAnnotations;

namespace FileProcessor.Data
{
    public class ApiKey
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Key { get; set; }

        public DateTime Expiration { get; set; }
    }
}
