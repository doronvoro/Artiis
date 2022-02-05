using Atriis.ProductManagement.BL;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Atriis.ProductManagement.Angular.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
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
        public async Task<IActionResult> Get([FromQuery] PageFilter pageFilter)
        {
            try
            {
                if (pageFilter == null) //todo: use ModelState.IsValid
                {
                    throw new ArgumentNullException(nameof(pageFilter));
                }

                var products = await _productService.GetPageResult(pageFilter);

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