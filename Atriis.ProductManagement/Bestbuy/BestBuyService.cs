using System.Net.Http.Json;

namespace Atriis.ProductManagement.BL
{
    public class PageResult<T>
    {
        public int Count { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public List<T> Items { get; set; }
    }
    public class PageFilter
    {
        public string TextToSearch { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }


    public class BestBuyService // todo: IProductService
    {
        private readonly HttpClient _httpClient;

        public BestBuyService(HttpClient httpClient)
        {
            var url = "https://api.bestbuy.com/";  //todo: add config option<>

            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri(url);
        }


        public async Task<PageResult<Product>?> GetProductsDetails(PageFilter pageFilter)
        {
            var url = $"v1/products(name={pageFilter.TextToSearch}*)?pageSize={pageFilter.PageSize}&page={pageFilter.PageIndex}&format=json&show=sku,name,salePrice,image,startDate&apiKey=VEu4DRF1Wwgl54oI4TerpOTq";
         
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
                PageIndex = pageFilter.PageIndex  
            };

            return pageResult;
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



    public class BestBuyProduct
    {
        public int sku { get; set; }
        public string name { get; set; }
        public double salePrice { get; set; }
        public string image { get; set; }
        public string startDate { get; set; }
    }

    public class BestBuyRoot
    {
        public int from { get; set; }
        public int to { get; set; }
        public int currentPage { get; set; }
        public int total { get; set; }
        public int totalPages { get; set; }
        public string queryTime { get; set; }
        public string totalTime { get; set; }
        public bool partial { get; set; }
        public string canonicalUrl { get; set; }
        public List<BestBuyProduct> products { get; set; }
    }
}