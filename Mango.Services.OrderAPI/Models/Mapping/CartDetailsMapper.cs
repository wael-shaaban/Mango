using AutoMapper;
using Mango.Services.OrderAPI.Models;
using MMango.Services.OrderAPI;

namespace Mango.Services.ShoppingCartAPI.Models.Mapping
{
    public class CartDetailsMapper:Profile
    {
        public CartDetailsMapper()
        {
            CreateMap<OrderDetailsModel, OrderDetailsDto>().PreserveReferences().ReverseMap();
            CreateMap<OrderDetailsDto,OrderDetailsModel>().PreserveReferences().ReverseMap();
        }
    }
}
