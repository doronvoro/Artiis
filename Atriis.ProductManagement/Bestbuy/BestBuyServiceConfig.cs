namespace Atriis.ProductManagement.BL
{
    public class BestBuyServiceConfig
    {
        public string BaseUrl { get; set; }

        public string ApiKey { get; set; }

        public override string ToString()
        {
            var toString = $"BaseUrl={BaseUrl} | ApiKey={ApiKey}";
            return toString;
        }
    }
}