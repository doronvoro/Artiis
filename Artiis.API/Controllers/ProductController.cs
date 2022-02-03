using Atriis.ProductManagement.BL;
using Microsoft.AspNetCore.Mvc;

namespace Artiis.API.Controllers
{
    // vs 22 v ??.??
    //instull node js
    //npm install -g @angular/cli

    //npm ERR! gyp verb `which` failed at FSReqCallback.oncomplete(node:fs:198:21) {
    //npm ERR! gyp verb `which` failed code: 'ENOENT'
    //npm ERR! gyp verb `which` failed
}

//todo: change project naem to product management API
//

public static class ControllerBaseExten
{
    public static ObjectResult InternalServerError(this ProductsController controller, Exception ex)
    {
        //todo: create base class for Controller or middlwhere
        //  controller._logger.LogCritical("", ex);
        // todo: fix this
        // todo: check for Exception details
        //todo: return error number for trecking
        var result = controller.StatusCode((int)StatusCodes.Status500InternalServerError, ex);
        return result;
    }


}

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
            return this.InternalServerError(ex);
        }
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> Test()
    {
        try
        {

            await Task.Delay(1);
            return Ok(new { message = "OK!!!" });
        }
        catch (Exception ex)
        {
            return this.InternalServerError(ex);
        }
    }


    [HttpGet("[action]")]
    public async Task<IActionResult> GetProductsDetails([FromQuery] PageFilter pageFilter)
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

}