using Mongo.Web.Services.IServices;
using Mongo.Web.Utility;

namespace Mongo.Web.Services
{
    public class ShoppingCartService(IBaseService _baseService) : IShoppingCartService
    {
        public async Task<GeneralResponseDTO?> ApplyCouponAsync(CartDto? cartDto)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ShoopingCartApiUrl + "/api/cart/ApplyCoupon",
                Data = cartDto
            });
        }

        public async Task<GeneralResponseDTO?> GetCartByUserIdAsync(string? userId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ShoopingCartApiUrl + "/api/cart/GetCart/"+userId,
            });
        }

        public async Task<GeneralResponseDTO?> RemoveFromCartAsync(int? cartDetailsId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ShoopingCartApiUrl + "/api/cart/RemoveCart",
                Data = cartDetailsId
            });
        }

        public async Task<GeneralResponseDTO?> UpsertCartAsync(CartDto? cartDto)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ShoopingCartApiUrl + "/api/cart/CartUpsert",
                Data = cartDto
            });
        }
        public async Task<GeneralResponseDTO?> CartEmail(CartDto? cartDto)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ShoopingCartApiUrl + "/api/cart/EmailCartRequest",
                Data = cartDto
            });
        }

    }
}
