using Atriis.ProductManagement.BL;
using Microsoft.AspNetCore.Mvc;

namespace Artiis.API.Controllers
{
    // vs 22
    //instull node js
    //npm install -g @angular/cli

    //npm ERR! gyp verb `which` failed at FSReqCallback.oncomplete(node:fs:198:21) {
    //npm ERR! gyp verb `which` failed code: 'ENOENT'
    //npm ERR! gyp verb `which` failed
}

//product management 
//
[ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductManager _productManager;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductManager productManager, 
                                 ILogger<ProductsController> logger)
        {
            _productManager = productManager;
            _logger = logger;
        }


       // [Route("~/api/[Controller]/Filter/{productName=}")]

       [HttpGet("[controller]")]
        public async Task<IActionResult> GetAll(string? productName = null)
        {
            try
            {
                var products = await _productManager.GetAll(productName);

                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("",ex); // todo: fix this
               
                return  StatusCode((int)StatusCodes.Status500InternalServerError, ex); 
            }
        }
    }
}