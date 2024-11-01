using Mongo.Web.Services.IServices;
using Mongo.Web.Utility;
using Mongo.Web.ViewModels;

namespace Mongo.Web.Services
{
    public class CouponService(IBaseService _baseService) : ICouponService
    {
        public async Task<GeneralResponseDTO?> CreateCouponAsync(CouponDTO newCoupon)
        {

            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.POST,
                Url = SD.CouponApiBaseUrl + "/api/Coupon/",
                Data = newCoupon
            });
        }

        public async Task<GeneralResponseDTO?> DeleteCouponAsync(int couponId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.CouponApiBaseUrl + "/api/Coupon/" + couponId
            });
        }

        public async Task<GeneralResponseDTO?> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponApiBaseUrl + "/api/Coupon"
            });
        }

        public async Task<GeneralResponseDTO?> GetCouponAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponApiBaseUrl + "/api/Coupon/GetByCode/" + couponCode
            });
        }

        public async Task<GeneralResponseDTO?> GetCouponByIdAsync(int couponId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponApiBaseUrl + "/api/Coupon/" + couponId
            });
        }

        public async Task<GeneralResponseDTO?> UpdateCouponAsync(CouponDTO coupon)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.PUT,
                Url = SD.CouponApiBaseUrl + "/api/Coupon/",
                Data = coupon
            });
        }
    }
}