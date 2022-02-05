using Atriis.ProductManagement.BL;
using Microsoft.AspNetCore.Mvc;

namespace Atriis.ProductManagement.Angular.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductDetailsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductDetailsController> _logger;

        public ProductDetailsController(IProductService productService,
                                        ILogger<ProductDetailsController> logger)

        {
            _productService = productService;
            _logger = logger;
        }
        

        [HttpGet("[action]")]
        [HttpGet]
        public async Task<IActionResult> Get(int sku)
        {
            try
            {
                var products = await _productService.GetProductDetails(sku);

                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, nameof(Get));
                return this.HandleExceptio(ex);
            }
        }

    }
}