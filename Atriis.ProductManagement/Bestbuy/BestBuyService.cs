using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Atriis.ProductManagement.BL
{
    public class BestBuyService  : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly BestBuyServiceConfig _serviceConfig;

        public BestBuyService(HttpClient httpClient, IOptions<BestBuyServiceConfig> options)
        {
            if (!(options?.IsValid() ?? false))
            {
                throw new ArgumentException( $"Invalid config value {options?.Value}");
            }

            _httpClient = httpClient;
            _serviceConfig = options.Value;
            _httpClient.BaseAddress = new Uri(_serviceConfig.BaseUrl);
        }

        public async Task<PageResult<Product>?> GetPageResult(PageFilter pageFilter)
        {
            var url = $"v1/products(name={pageFilter.TextToSearch}*)?pageSize={pageFilter.PageSize}&page={pageFilter.PageIndex}&"+
                      $"format=json&show=sku,name,salePrice,image&sort={pageFilter.SortCoulmn}&apiKey={_serviceConfig.ApiKey}";

            var data = await _httpClient.GetFromJsonAsync<BestBuyRoot>(url );

            var products = data.products.Select(s => new Product
            {
                Image = s.image,
                Name = s.name,
                Price = s.salePrice,
                Sku = s.sku
            });

            if(products.Count () > pageFilter.PageSize)
            {
                throw new Exception("pagesize not valid"); //todo: create custom Exception
            }

            var pageResult = new PageResult<Product>
            {
                Count = data.total,
                PageSize = pageFilter.PageSize,
                Items = products.ToList(),
                PageIndex = pageFilter.PageIndex,
                TotalPage = data.totalPages
            };

            return pageResult;
        }

        public async Task<ProductDetail> GetProductDetails(int sku)
        {
            if (sku <= 0)
            {
                throw new ArgumentException($"Invalid sku value[sku={sku}]");
            }
            var url = $"v1/products/{sku}.json?apiKey={_serviceConfig.ApiKey}";

            var data = await _httpClient.GetFromJsonAsync<BestBuyProducDetail>(url);

            var productDetail = new ProductDetail
            {
                //Description = data.plot,
                Name = data.Name,
                Sku = data.Sku,
                Price = data.SalePrice,
                Description = data.LongDescription ?? data.ShortDescription,

                Images = data.Images?.Select(s => s.href)?.ToArray(),
            };

            return productDetail;
        }
    }
}