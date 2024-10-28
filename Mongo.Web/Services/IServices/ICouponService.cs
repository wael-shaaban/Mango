using Mongo.Web.ViewModels;

namespace Mongo.Web.Services.IServices
{
    public interface ICouponService
    {
        public Task<GeneralResponseDTO?> GetCouponAsync(string couponCode);
        public Task<GeneralResponseDTO?> GetCouponByIdAsync(int couponId);
        public Task<GeneralResponseDTO?> GetAllCouponsAsync();
        public Task<GeneralResponseDTO?> UpdateCouponAsync(CouponDTO coupon);
        public Task<GeneralResponseDTO?> DeleteCouponAsync(int couponId);
        public Task<GeneralResponseDTO?> CreateCouponAsync(CouponDTO newCoupon);
    }
}