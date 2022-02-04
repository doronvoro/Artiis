namespace Atriis.ProductManagement.BL
{
    public interface IProductService
    {
        public   Task<PageResult<Product>?> GetPageResult(PageFilter pageFilter);
    }
}