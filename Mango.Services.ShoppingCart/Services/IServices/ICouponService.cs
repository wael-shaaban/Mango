using Mango.Services.ShoppingCartAPI.DTOs;

namespace Mango.Services.ShoppingCartAPI.Services.IServices
{
    public interface ICouponService
    {
        public Task<CouponDTO> GetCoupon(string? couponCode);
    }
}
