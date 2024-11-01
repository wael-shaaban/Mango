using Mongo.Web.Services.IServices;
using Mongo.Web.Utility;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace Mongo.Web.Services
{
    public class ProductService(IBaseService _baseService) : IProductService
    {
        public Task<GeneralResponseDTO?> CreateProductAsync(ProductDTO newProduct)
        {
            return _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ProductApiBaseUrl + "/api/Product/",
                Data = newProduct
            });
        }

        public Task<GeneralResponseDTO?> DeleteProductAsync(int productId)
        {
            return _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ProductApiBaseUrl + "/api/Product/" + productId
            });
        }

        public Task<GeneralResponseDTO?> GetAllProductsAsync()
        {
            return _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductApiBaseUrl + "/api/Product"
            });
        }

        public Task<GeneralResponseDTO?> GetProductAsync(string productName)
        {
            return _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductApiBaseUrl + "/api/Product/GetByName/" + productName
            });
        }

        public Task<GeneralResponseDTO?> GetProductByIdAsync(int productId)
        {
            return _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductApiBaseUrl + "/api/Product/" + productId
            });
        }

        public Task<GeneralResponseDTO?> UpdateProductAsync(ProductDTO product)
        {
            return _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.PUT,
                Url = SD.ProductApiBaseUrl + "/api/Product/",
                Data = product
            });
        }
    }
}