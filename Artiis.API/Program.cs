

using Atriis.ProductManagement.BL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


 
////////////////todo: move to bl project/////////

builder.Services.Configure<BestBuyServiceConfig>(
builder.Configuration.GetSection(nameof(BestBuyServiceConfig)));

builder.Services.AddHttpClient<BestBuyService>();
builder.Services.AddTransient<IProductService, BestBuyService>();


builder.Services.AddProductManagementBL();


//IProductManager
builder.Services.AddProductManagementBL();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
