using AutoMapper;
using Mango.Services.CouponAPI.DTOs;
using Mango.Services.CouponAPI.Models;

namespace Mango.Services.CouponAPI.Mapping
{
    public class CouponMapper : Profile
    {
        public CouponMapper()
        {
            this.CreateMap<CouponModel, CouponDTO>().PreserveReferences().ReverseMap();
            this.CreateMap<CouponDTO, CouponModel>().PreserveReferences().ReverseMap();
        }
    }
}