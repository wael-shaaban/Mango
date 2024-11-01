using AutoMapper;
using Mango.Services.ShoppingCartAPI.DTOs;

namespace Mango.Services.ShoppingCartAPI.Models.Mapping
{
    public class CartHeaderMapper:Profile
    {
        public CartHeaderMapper()
        {
            CreateMap<CartHeaderModel, CartHeaderDto>().PreserveReferences().ReverseMap();
            CreateMap<CartHeaderDto, CartHeaderModel>().PreserveReferences().ReverseMap();
        }
    }
}
