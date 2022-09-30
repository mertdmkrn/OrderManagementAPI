using Microsoft.AspNetCore.Mvc;
using Nest;
using OrderManagement.Models;

namespace OrderManagement.API.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ElasticClient _client;

        public ProductController(ElasticClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Get Products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/product")]
        public IActionResult getProducts(string barcode)
        {
            ISearchResponse<Product> results;

            if (!string.IsNullOrWhiteSpace(barcode) || barcode.Equals("hepsi"))
            {
                results = _client.Search<Product>(s => s
                    .Query(q => q
                    .Term(t => t
                            .Field(f => f.Barcode)
                            .Value(barcode)
                        )
                    )
                );
            }
            else
            {
                results = _client.Search<Product>(s => s
                    .Query(q => q
                        .MatchAll()
                    )
                );
            }

            return Ok(results);
        }

    }
}
