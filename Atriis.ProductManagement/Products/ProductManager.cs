using Microsoft.Extensions.Configuration;

namespace Atriis.ProductManagement.BL
{
    public class ProductManager : IProductManager
    {
        private readonly BestBuyService _bestBuyService;
        public ProductManager(BestBuyService bestBuyService)
        {
            _bestBuyService = bestBuyService;
        }

        public async Task<IEnumerable<Product>?> GetAll(string productName)
        {
            var products = await _bestBuyService.GetProductsAsync(productName);

            return products;
        }

        public async Task<PageResult<Product>?> GetProductsDetails(PageFilter pageFilter)
        {
            var products = await _bestBuyService.GetPageResult(pageFilter);

            return products;
        }
    }
}