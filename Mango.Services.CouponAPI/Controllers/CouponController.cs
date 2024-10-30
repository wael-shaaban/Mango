using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.DTOs;
using Mango.Services.CouponAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/Coupon")]
    [ApiController]
    [Authorize]
    public class CouponController(AppDbContext _dbContext, IMapper _mapper) : ControllerBase
    {
        protected GeneralResponseDTO generalResponse = new GeneralResponseDTO();
        [HttpGet]
        public GeneralResponseDTO GetAllCoupons()
        {
            try
            {
                var data = _dbContext.Coupons.ToList();
                var result = _mapper.Map<IEnumerable<CouponDTO>>(data);
                if (data is null)
                    throw new Exception("Data Not Found");
                generalResponse.Data = result;
                generalResponse.Success = true;
                return generalResponse;
            }
            catch (Exception ex)
            {
                generalResponse.Message = ex.Message;
                return generalResponse;
            }
        }
        [HttpGet("{id:int}")]
        public GeneralResponseDTO GetById(int id)
        {
            try
            {
                var data = _dbContext.Coupons.FirstOrDefault(c => c.CouponId == id); 
                if (data is null)
                    throw new Exception("Data Not Found");
                var result = _mapper.Map<CouponDTO>(data);          
                generalResponse.Data = result;
                generalResponse.Success = true;
                return generalResponse;
            }
            catch (Exception ex)
            {
                generalResponse.Message = ex.Message;
                return generalResponse;
            }
        }
        [HttpGet("GetByCode/{code:regex(^[[a-zA-Z0-9]]+$)}")]
        public GeneralResponseDTO GetByCode(string code)
        {
            try
            {
                var data = _dbContext.Coupons.FirstOrDefault(c => c.CouponCode == code);
                var result = _mapper.Map<CouponDTO>(data);
                if (data is null)
                    throw new Exception("Data Not Found");
                generalResponse.Data = result;
                generalResponse.Success = true;
                return generalResponse;
            }
            catch (Exception ex)
            {
                generalResponse.Message = ex.Message;
                return generalResponse;
            }
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public GeneralResponseDTO AddCoupon(CouponDTO coupon)
        {
            try
            {
                if (coupon is  null)
                    throw new Exception("Coupon data is null");
                var result = _mapper.Map<CouponModel>(coupon);
                if (result == null)
                    throw new Exception("Data Not Valid");
                _dbContext.Coupons.Add(result);
                _dbContext.SaveChanges();
                generalResponse.Success = true;
                generalResponse.Data = _mapper.Map<CouponDTO>(result);

                // Returns a 201 status code with a location header pointing to the GetById method.
                //return CreatedAtAction(nameof(GetById), new { id = result.CouponId }, generalResponse);
                return generalResponse;
            }
            catch (Exception ex)
            {
                generalResponse.Success = false;
                generalResponse.Message = ex.Message;
                return generalResponse;
            }
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public GeneralResponseDTO EditCoupon(CouponDTO coupon)
        {
            try
            {
                if (coupon == null)
                {
                    throw new Exception("Coupon data is null");
                }

                var result = _mapper.Map<CouponModel>(coupon);
                if (result == null)
                    throw new Exception("Data Not Valid");
                _dbContext.Coupons.Update(result);
                _dbContext.SaveChanges();
                generalResponse.Success = true;
                generalResponse.Data = _mapper.Map<CouponDTO>(result);

                // Returns a 201 status code with a location header pointing to the GetById method.
                return generalResponse;  
            }
            catch (Exception ex)
            {
                generalResponse.Success = false;
                generalResponse.Message = ex.Message;
                return generalResponse;
            }
        }
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public GeneralResponseDTO RemoveCoupon(int id)
        {
            try
            {
                var data = _dbContext.Coupons.FirstOrDefault(c => c.CouponId == id);
                if (data is null)
                    throw new Exception("Data Not Found");
                _dbContext.Coupons.Remove(data);
                _dbContext.SaveChanges();
                generalResponse.Success = true; 
                return generalResponse;
            }
            catch (Exception ex)
            {
                generalResponse.Message = ex.Message;
                return generalResponse;
            }
        }
       

    }
}