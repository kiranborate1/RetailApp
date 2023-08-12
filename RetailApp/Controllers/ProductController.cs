using Microsoft.AspNetCore.Mvc;
using RetailApp.DTO;
using RetailApp.Interfaces;
using RetailApp.Model;

namespace RetailApp.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetActiveProducts")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetActiveProducts()
        {
           return Ok(await _productService.GetActiveProducts());
        }

        [HttpGet("search")]
        public IActionResult SearchProducts([FromQuery] string productName, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice, [FromQuery] DateTime? minPostedDate, [FromQuery] DateTime? maxPostedDate)
        {
            return Ok(_productService.SearchProducts(productName, minPrice, maxPrice, minPostedDate, maxPostedDate));
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            return Ok(_productService.CreateProduct(product));
        }

        [HttpPut("{productId}")]//I just tried this thru stored procedure
        public IActionResult UpdateProduct(int productId, [FromBody] ProductDTO productDto)
        {
            var product = _productService.UpdateProductWithApproval(productId,productDto);

            return Ok(product);
        }

        [HttpDelete("{productId}")]
        public IActionResult DeleteProduct(int productId)
        {
            _productService.DeleteProductAndPushToApproval(productId);
            return NoContent(); 
        }

        [HttpGet("approval-queue")]
        public IActionResult GetProductsInApprovalQueue()
        {
            var productsInApprovalQueue = _productService.GetProductsInApprovalQueue();
            return Ok(productsInApprovalQueue);
        }
    }
}
