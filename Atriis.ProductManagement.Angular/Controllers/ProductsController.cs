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
        //'/weatherforecast' remove

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PageFilter pageFilter)
        {
            try
            {
                var products = await _productManager.GetProductsDetails(pageFilter);

                return Ok(products);
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }


        //[HttpGet]
        //public async Task<IActionResult> Get(string? productName = null)
        //{
        //    try
        //    {
        //        var products = await _productManager.GetAll(productName);

        //        return Ok(products);
        //    }
        //    catch (Exception ex)
        //    {
        //        return this.InternalServerError(ex);
        //    }
        //}


        //[HttpGet("[action]")]
        //public async Task<IActionResult> Test( )
        //{
        //    try
        //    {

        //        await Task.Delay(1);
        //        return Ok(new  { message = "OK!!!"} );
        //    }
        //    catch (Exception ex)
        //    {
        //        return this.InternalServerError(ex);
        //    }
        //}

        //[HttpGet("[action]")]
        //public async Task<IActionResult> GetProductsDetails([FromQuery] PageFilter pageFilter)
        //{
        //    try
        //    {
        //        var products = await _productManager.GetProductsDetails(pageFilter);

        //        return Ok(products);
        //    }
        //    catch (Exception ex)
        //    {
        //        return this.InternalServerError(ex);
        //    }
        //}

    }

}