namespace Mongo.Web.Services.IServices
{
    public interface IProductService
    {
        public Task<GeneralResponseDTO?> GetProductAsync(string productName);
        public Task<GeneralResponseDTO?> GetProductByIdAsync(int productId);
        public Task<GeneralResponseDTO?> GetAllProductsAsync();
        public Task<GeneralResponseDTO?> UpdateProductAsync(ProductDTO product);
        public Task<GeneralResponseDTO?> DeleteProductAsync(int productId);
        public Task<GeneralResponseDTO?> CreateProductAsync(ProductDTO newProduct);
    }
}