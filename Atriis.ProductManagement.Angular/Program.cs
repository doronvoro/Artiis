using Atriis.ProductManagement.BL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

////////////////todo: move to bl project/////////

builder.Services.Configure<BestBuyServiceConfig>(
builder.Configuration.GetSection(nameof(BestBuyServiceConfig)));

builder.Services.AddHttpClient<BestBuyService>();
//.Services.AddTransient<IProductManager, ProductManager>();
builder.Services.AddTransient<IProductService, BestBuyService>();

 
builder.Services.AddProductManagementBL();
//////////////////////////////////////////////

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
