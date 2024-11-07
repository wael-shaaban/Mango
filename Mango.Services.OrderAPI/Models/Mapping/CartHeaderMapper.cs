using AutoMapper;
using Mango.Services.OrderAPI.Models;
using MMango.Services.OrderAPI;

namespace Mango.Services.ShoppingCartAPI.Models.Mapping
{
    public class CartHeaderMapper:Profile
    {
        public CartHeaderMapper()
        {
            CreateMap<OrderHeaderModel, OrderHeaderDto>().PreserveReferences().ReverseMap();
            CreateMap<OrderHeaderDto, OrderHeaderModel>().PreserveReferences().ReverseMap();
        }
    }
}
