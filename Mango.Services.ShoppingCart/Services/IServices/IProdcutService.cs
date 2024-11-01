using Mango.Services.ShoppingCartAPI.DTOs;

namespace Mango.Services.ShoppingCartAPI.Services.IServices
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDTO>> GetProductsAsync();
    }
}