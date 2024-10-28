using Mongo.Web.Services.IServices;
using Mongo.Web.Utility;
using Mongo.Web.ViewModels;

namespace Mongo.Web.Services
{
    public class CouponService(IBaseService _baseService) : ICouponService
    {
        public Task<GeneralResponseDTO?> CreateCouponAsync(CouponDTO newCoupon)
        {

            return _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.POST,
                Url = SD.CouponApiBaseUrl + "/api/Coupon/",
                Data = newCoupon
            });
        }

        public Task<GeneralResponseDTO?> DeleteCouponAsync(int couponId)
        {
            return _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.CouponApiBaseUrl + "/api/Coupon/" + couponId
            });
        }

        public Task<GeneralResponseDTO?> GetAllCouponsAsync()
        {
            return _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponApiBaseUrl + "/api/Coupon"
            });
        }

        public Task<GeneralResponseDTO?> GetCouponAsync(string couponCode)
        {
            return _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponApiBaseUrl + "/api/Coupon/GetByCode/" + couponCode
            });
        }

        public Task<GeneralResponseDTO?> GetCouponByIdAsync(int couponId)
        {
            return _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponApiBaseUrl + "/api/Coupon/" + couponId
            });
        }

        public Task<GeneralResponseDTO?> UpdateCouponAsync(CouponDTO coupon)
        {
            return _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.PUT,
                Url = SD.CouponApiBaseUrl + "/api/Coupon/",
                Data = coupon
            });
        }
    }
}