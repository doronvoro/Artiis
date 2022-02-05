using Microsoft.Extensions.Options;

namespace Atriis.ProductManagement.BL
{
    public static class BestBuyServiceConfigExtension
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
}