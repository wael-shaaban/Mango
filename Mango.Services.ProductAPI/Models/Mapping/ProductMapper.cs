using AutoMapper;
using Mango.Services.ProductAPI.DTOs;
using System.Data;

namespace Mango.Services.ProductAPI.Models.Mapping
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<ProductModel, ProductDTO>().PreserveReferences().ReverseMap();
            CreateMap<ProductDTO, ProductModel>().PreserveReferences().ReverseMap();
        }
    }
}