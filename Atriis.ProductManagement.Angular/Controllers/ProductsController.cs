using Atriis.ProductManagement.BL;
using Microsoft.AspNetCore.Mvc;

namespace Atriis.ProductManagement.Angular.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService,
                                  ILogger<ProductsController> logger)
                                
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PageFilter pageFilter)
        {
            try
            {
                var products = await _productService.GetPageResult(pageFilter);

                return Ok(products);
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductDetails(int sku)
        {
            try
            {
                var products = await _productService.GetProductDetails(sku);

                return Ok(products);
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }

    }
}