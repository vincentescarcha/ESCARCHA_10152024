using FileProcessor.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileProcessor.Controllers
{
    [ApiController]
    [Route("api/products")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public class ProductController : ControllerBase
    {
        private ProductServices _productServices;
        public ProductController(ProductServices productServices)
        {
            _productServices = productServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _productServices.GetProducts());
        }
    }
}
