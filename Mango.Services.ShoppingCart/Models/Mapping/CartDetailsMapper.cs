using AutoMapper;
using Mango.Services.ShoppingCartAPI.DTOs;

namespace Mango.Services.ShoppingCartAPI.Models.Mapping
{
    public class CartDetailsMapper:Profile
    {
        public CartDetailsMapper()
        {
            CreateMap<CartDetailsModel, CartDetailsDto>().PreserveReferences().ReverseMap();
            CreateMap<CartDetailsDto,CartDetailsModel>().PreserveReferences().ReverseMap();
        }
    }
}
