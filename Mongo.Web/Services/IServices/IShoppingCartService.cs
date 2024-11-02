namespace Mongo.Web.Services.IServices
{
    public interface IShoppingCartService
    {
        public Task<GeneralResponseDTO?> GetCartByUserIdAsync(string? userId);    
        public Task<GeneralResponseDTO?> UpsertCartAsync(CartDto? cartDto);    
        public Task<GeneralResponseDTO?> RemoveFromCartAsync(int? cartDetailsId);    
        public Task<GeneralResponseDTO?> ApplyCouponAsync(CartDto? cartDto);    
        public Task<GeneralResponseDTO?> CartEmail(CartDto? cartDto);    
    }
}
