﻿using Mongo.Web.Services.IServices;
using Mongo.Web.Utility;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace Mongo.Web.Services
{
    public class ProductService(IBaseService _baseService) : IProductService
    {
        public async Task<GeneralResponseDTO?> CreateProductAsync(ProductDTO newProduct)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ProductApiBaseUrl + "/api/Product/",
                Data = newProduct
            });
        }

        public async Task<GeneralResponseDTO?> DeleteProductAsync(int productId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ProductApiBaseUrl + "/api/Product/" + productId
            });
        }

        public async Task<GeneralResponseDTO?> GetAllProductsAsync()
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductApiBaseUrl + "/api/Product"
            });
        }

        public async Task<GeneralResponseDTO?> GetProductAsync(string productName)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductApiBaseUrl + "/api/Product/GetByName/" + productName
            });
        }

        public async Task<GeneralResponseDTO?> GetProductByIdAsync(int productId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductApiBaseUrl + "/api/Product/" + productId
            });
        }

        public async Task<GeneralResponseDTO?> UpdateProductAsync(ProductDTO product)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.PUT,
                Url = SD.ProductApiBaseUrl + "/api/Product/",
                Data = product
            });
        }
    }
}