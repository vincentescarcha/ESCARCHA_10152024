using FileProcessor.Data;

namespace FileProcessor.Services
{
    public class ProductServices
    {
        private readonly FileProcessorDbContext _context;
        public ProductServices(FileProcessorDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProducts()
        {
            return _context.Products.ToList();
        }
    }
}
