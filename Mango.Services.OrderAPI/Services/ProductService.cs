using MMango.Services.OrderAPI;
using Mango.Services.ShoppingCartAPI.Services.IServices;
using Newtonsoft.Json;
using Mango.Services.OrderAPI.DTOs;

namespace Mango.Services.ShoppingCartAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(IHttpClientFactory httpClientFactory) =>
                   _httpClient = httpClientFactory.CreateClient("Product");

        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            var apiResponse = await _httpClient.GetFromJsonAsync<GeneralResponseDTO>("/api/Product");
            //var apiResponse = await _httpClient.GetAsync($"/api/Product");
            //var apiContent = await apiResponse.Content.ReadAsStringAsync();
            //var res = JsonConvert.DeserializeObject<GeneralResponseDTO>(apiContent);
            return apiResponse.Success ? JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(Convert.ToString(apiResponse.Data))
                : new List<ProductDTO>();
        }
    }
}