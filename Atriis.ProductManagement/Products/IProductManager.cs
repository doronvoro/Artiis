namespace Atriis.ProductManagement.BL
{

    public interface IProductManager
    {
        public   Task<IEnumerable<Product>?> GetAll(string productName);

        public Task<PageResult<Product>?> GetProductsDetails(PageFilter pageFilter);

    }



}