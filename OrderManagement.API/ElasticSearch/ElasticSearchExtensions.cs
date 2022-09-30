using Nest;
using OrderManagement.Models;
using OrderManagement.Services;

namespace OrderManagement.API.ElasticSearch
{
    public static class ElasticSearchExtensions
    {
        public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["elasticsearch:url"];
            var defaultIndex = configuration["elasticsearch:index"];
  
            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(defaultIndex);
  
            AddDefaultMappings(settings);
  
            var client = new ElasticClient(settings);
  
            services.AddSingleton(client);
  
            CreateIndex(client, defaultIndex);
        }
  
        private static void AddDefaultMappings(ConnectionSettings settings)
        {
            settings.
                DefaultMappingFor<Product>(m => m
                    .Ignore(p => p.Barcode)
                    .Ignore(p => p.Description)
                    .Ignore(p => p.Price)
                );
        }
  
        private static void CreateIndex(IElasticClient client, string indexName)
        {
            var createIndexResponse = client.Indices.Create(indexName,
                index => index.Map<Product>(x => x.AutoMap())
            );

            ProductService productService = new ProductService();
            client.IndexMany(productService.GetAllProduct(),indexName);
        }
    }
}
