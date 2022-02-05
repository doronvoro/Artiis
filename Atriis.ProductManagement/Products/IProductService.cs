using System.ComponentModel.DataAnnotations;

namespace Atriis.ProductManagement.BL
{
    public interface IProductService
    {
        public Task<PageResult<Product>?> GetPageResult(PageFilter pageFilter);

        public Task<ProductDetail> GetProductDetails(int sku);
    }
}