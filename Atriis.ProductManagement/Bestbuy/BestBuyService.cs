using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Atriis.ProductManagement.BL
{
    public static class Ext
    {
        public static bool IsValid(this IOptions<BestBuyServiceConfig> options)
        {
            if (options?.Value == null)
            {
                return false;
            }

            if(options?.Value.ApiKey == null)
            {
                return false;

            }
            if (options?.Value.BaseUrl == null)
            {
                return false;

            }
            return true;
        }
    }


    public class BestBuyProducDetail
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string StartDate { get; set; }

        public double SalePrice { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }


        public int Sku { get; set; }

   

        public List<Image> Images { get; set; }


    }

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
//            var url = $"v1/products(name={pageFilter.TextToSearch}*)?pageSize={pageFilter.PageSize}&page={pageFilter.PageIndex}&format=json&show=sku,name,salePrice,image,startDate&apiKey=VEu4DRF1Wwgl54oI4TerpOTq";
            var url = $"v1/products(name={pageFilter.TextToSearch}*)?pageSize={pageFilter.PageSize}&page={pageFilter.PageIndex}&format=json&show=sku,name,salePrice,image&sort=sku&apiKey={_serviceConfig.ApiKey}";

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
                throw new Exception("pagesize not valid");
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

        public async Task<IEnumerable<Product>?> GetProductsAsync(string name)
        {
            //todo:
            // salePrice ? price
            // filter name = null
            // pageSize?
            // create custom filter => IProduct filter
            // get api key form config
            // test for missing configuration
            // test for moq filter
            // nuget for demo product name
            // check for map fields
            var data = await _httpClient.GetFromJsonAsync<BestBuyRoot>(
                             //"v1/products(name=\"*Greenberg*\")?pageSize=50&page=2&format=json&show=sku,name,salePrice,image,startDate&apiKey=VEu4DRF1Wwgl54oI4TerpOTq"
                             $"v1/products(name={name}*)?pageSize=50&page=2&format=json&show=sku,name,salePrice,image,startDate&apiKey=VEu4DRF1Wwgl54oI4TerpOTq"
                             );



            var products = data.products.Select(s => new Product
            {
                Image = s.image,
                Name = s.name,
                Price = s.salePrice,
                Sku = s.sku
            });
            

            return products;



        }
       
    }
}