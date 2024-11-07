using MMango.Services.OrderAPI;
using Mango.Services.ShoppingCartAPI.Services.IServices;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ShoppingCartAPI.Services
{
    public class CouponService : ICouponService
    {
        private readonly HttpClient _httpClient;
        public CouponService(IHttpClientFactory httpClientFactory) =>
                   _httpClient = httpClientFactory.CreateClient("Coupon");
        public async Task<CouponDTO> GetCoupon([RegularExpression("^[[a-zA-Z0-9]]+$")] string? couponCode)
        {
            var apiResponse = await _httpClient.GetFromJsonAsync<GeneralResponseDTO>($"/api/Coupon/GetByCode/{couponCode}");
            return apiResponse.Success ? JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(apiResponse.Data))
          : new CouponDTO();
        }
    }
}