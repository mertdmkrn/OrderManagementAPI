using OrderManagement.DataAccess.Repository;
using OrderManagement.DataAccess.Repository.Abstract;
using OrderManagement.Services.Abstract;
using OrderManagement.Services;
using Serilog;
using Microsoft.Extensions.Configuration;
using OrderManagement.API.ElasticSearch;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var currentPath = Path.Combine(AppContext.BaseDirectory.Replace("bin\\Debug\\net6.0\\", ""));

        var logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File(Path.Combine(currentPath, "logs/log.txt"), rollingInterval: RollingInterval.Day)
            .CreateLogger();

        builder.Host.UseSerilog(logger);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(c =>
        {
            var filePath = Path.Combine(currentPath + "OrderManagementApi.xml");
            c.IncludeXmlComments(filePath);
        });

        builder.Services.AddElasticsearch(builder.Configuration);

        builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
        builder.Services.AddSingleton<IOrderRepository, OrderRepository>();
        builder.Services.AddSingleton<IProductRepository, ProductRepository>();
        builder.Services.AddSingleton<IOrderItemRepository, OrderItemRepository>();

        builder.Services.AddSingleton<ICustomerService, CustomerService>();
        builder.Services.AddSingleton<IOrderService, OrderService>();
        //builder.Services.AddSingleton<IProductService, ProductService>();
        builder.Services.AddSingleton<IOrderItemService, OrderItemService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}