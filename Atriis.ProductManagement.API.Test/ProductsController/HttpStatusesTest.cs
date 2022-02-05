using Atriis.ProductManagement.Angular.Controllers;
using Atriis.ProductManagement.BL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace Atriis.ProductManagement.API.Test
{
    public class ProductsController_Http_Statuses_Test
    {
        [Fact]
        public async void ProductsController_Returm_Ststus_200_When_ProductService_Returen_Valid_Data()
        {
            // Arrange
            var pageFilter = new PageFilter { };

            var mockProductService = new Mock<IProductService>();

            mockProductService.Setup(repo => repo.GetPageResult(pageFilter))
                              .ReturnsAsync(() => new PageResult<Product>
                              {
                                  Count = 1,
                                  PageIndex = 1,
                                  PageSize = 5,
                                  TotalPage = 1,
                                  Items = new List<Product> { new Product { } }
                              });



            var mockLogger = new Mock<ILogger<ProductsController>>();
            var controller = new ProductsController(mockProductService.Object, mockLogger.Object);

            // Act
            var actionResult = await controller.Get(pageFilter);
            var okResult = actionResult as OkObjectResult;


            // Assert
            Assert.NotNull(okResult);
            Assert.IsType<OkObjectResult>(okResult);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async void ProductsController_Returm_Ststus_500_When_ProductService_Throw_Exception()
        {
            // Arrange
            var exceptionMessage = $"{nameof(IProductService)}_from_unit_test";
            var pageFilter = new PageFilter { };
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(repo => repo.GetPageResult(pageFilter))
                              .ReturnsAsync(() =>
                              {
                                  throw new Exception(exceptionMessage);
                              });

            var mockLogger = new Mock<ILogger<ProductsController>>();
            var controller = new ProductsController(mockProductService.Object, mockLogger.Object);

            // Act
            var actionResult = await controller.Get(pageFilter);
            var result = actionResult as ObjectResult;

            // Assert
            Assert.IsType<ObjectResult>(actionResult);
            Assert.NotNull(result);
            Assert.Equal(exceptionMessage, (result.Value as Exception).Message); // only on dev mode
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async void ProductsController_Returm_Ststus_400_When_Request_Is_Empty()
        {
            // Arrange
            var pageFilter = null as PageFilter;
            var pageResult = null as PageResult<Product>;

            var mockProductService = new Mock<IProductService>();

            mockProductService.Setup(repo => repo.GetPageResult(pageFilter))
                              .ReturnsAsync(() => pageResult);



            var mockLogger = new Mock<ILogger<ProductsController>>();
            var controller = new ProductsController(mockProductService.Object, mockLogger.Object);

            // Act
            var actionResult = await controller.Get(pageFilter) as ObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.Equal((int)HttpStatusCode.UnprocessableEntity, actionResult.StatusCode);
        }
    }
}