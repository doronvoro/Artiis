using Microsoft.AspNetCore.Mvc;

namespace Atriis.ProductManagement.Angular.Controllers
{
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

}