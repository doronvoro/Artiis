using Microsoft.AspNetCore.Mvc;

namespace Atriis.ProductManagement.Angular.Controllers
{
    public static class ControllerExtension
    {
        //todo: use app.UseExceptionHandler("/error");
        public static ObjectResult HandleExceptio(this  Controller controller, Exception ex)
        {
            var result = null as  ObjectResult ;

            if (ex is ArgumentNullException)
            {
                result = controller.StatusCode((int)StatusCodes.Status422UnprocessableEntity, ex);

            }
            else
            {
                result = controller.StatusCode((int)StatusCodes.Status500InternalServerError, ex);
            }
            return result;
        }
    }

}