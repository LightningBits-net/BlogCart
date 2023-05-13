using BlogCart.Service.IService;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SharedServices.Models;

namespace BlogCart.Service
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private IConfiguration? _configuration;
        private string? BaseServerUrl;

        public ProductService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            BaseServerUrl = _configuration.GetSection("BaseServerUrl").Value;
        }

        public async Task<ProductDTO> Get(int productId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/Api/product/{productId}");
                var content = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var product = JsonConvert.DeserializeObject<ProductDTO>(content);
                    product.ImageUrl = BaseServerUrl + product.ImageUrl;
                    return product;
                }
                else
                {
                    var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                    throw new Exception(errorModel.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting the product: {ex.Message}");
                return new ProductDTO();
            }
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            try
            {
                var response = await _httpClient.GetAsync("/Api/Product");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var products = JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(content);
                    foreach (var prod in products)
                    {
                        prod.ImageUrl = BaseServerUrl + prod.ImageUrl;
                    }
                    return products;
                }
                else
                {
                    return Enumerable.Empty<ProductDTO>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting all products: {ex.Message}");
                return Enumerable.Empty<ProductDTO>();
            }
        }
    }
}

