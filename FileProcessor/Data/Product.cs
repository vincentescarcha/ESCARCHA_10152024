using System.ComponentModel.DataAnnotations;

namespace FileProcessor.Data
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }

        [Required]
        public string Name { get; set; }
        public string ImageName { get; set; }
        public string Category { get; set; }

        [Required]
        public double Price { get; set; }
        public double Discount { get; set; }
    }
}
