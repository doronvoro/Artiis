using Atriis.ProductManagement.BL;
using Microsoft.AspNetCore.Mvc;

namespace Atriis.ProductManagement.Angular.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductManager _productManager;
        protected readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductManager productManager,
                                 ILogger<ProductsController> logger)
        {
            _productManager = productManager;
            _logger = logger;
        }


        // [Route("~/api/[Controller]/Filter/{productName=}")]
        //todo:
        //fix Route 
        //[controller] => to function
        //check new error for ? nullbale
        [HttpGet]
        public async Task<IActionResult> Get(string? productName = null)
        {
            try
            {
                var products = await _productManager.GetAll(productName);

                return Ok(products);
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }

    }

}