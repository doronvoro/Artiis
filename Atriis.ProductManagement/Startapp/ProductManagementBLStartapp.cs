using Microsoft.Extensions.DependencyInjection;

namespace Atriis.ProductManagement.BL
{
    public static class ProductManagementBLStartapp
    {
        //todo: fix this..
        public static IServiceCollection AddProductManagementBL(this IServiceCollection services)
        {
            //services.AddTransient<IProductService, BestBuyService>();


            //services.AddHttpClient<BestBuyService>();
            ////.Services.AddTransient<IProductManager, ProductManager>();
            //builder.Services.AddTransient<IProductService, BestBuyService>();

            ////  services.AddHttpClient<BestBuyService>();


            // services.AddHttpClient<BestBuyService>();

            return services;
        }

    }
}
