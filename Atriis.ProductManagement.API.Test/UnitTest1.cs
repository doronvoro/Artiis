using Atriis.ProductManagement.Angular.Controllers;
using Atriis.ProductManagement.BL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using Xunit;

namespace Atriis.ProductManagement.API.Test
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            var pageFilter = new PageFilter { };

            // Arrange
            var mockProductService = new Mock<IProductService>();

            mockProductService.Setup(repo => repo.GetPageResult(pageFilter))
                     .ReturnsAsync(() => new PageResult<Product>
                     {
                         Count = 1,
                         PageIndex = 1,
                         PageSize = 5,
                         TotalPage= 1,
                         Items =  new List<Product> { new Product { } }

                     });



            var mockLogger = new Mock<ILogger<ProductsController>>();
            var controller = new ProductsController(mockProductService.Object, mockLogger.Object);


            //Microsoft.AspNetCore.Mvc.OkObjectResult
            // Act
            var actionResult = await controller.Get(pageFilter);

            var a = actionResult as IActionResult;
            var b = actionResult as Microsoft.AspNetCore.Mvc.OkObjectResult;



            var contentResult = actionResult as NegotiatedContentResult<Product>;

         

            // Assert
            Assert.NotNull(actionResult);
           Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal((int)HttpStatusCode.OK, b.StatusCode);
            //Assert.NotNull(contentResult.Content);
            // Assert.Equal(10, contentResult.Content.Id);

        }
    }
}