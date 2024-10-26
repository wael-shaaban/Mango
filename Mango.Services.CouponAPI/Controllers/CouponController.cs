using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.DTOs;
using Mango.Services.CouponAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController(AppDbContext _dbContext, IMapper _mapper) : ControllerBase
    {
        private readonly GeneralResponseDTO generalResponse = new GeneralResponseDTO();
        [HttpGet]
        public ActionResult<GeneralResponseDTO> GetAllCoupons()
        {
            try
            {
                var data = _dbContext.Coupons.ToList();
                var result = _mapper.Map<IEnumerable<CouponDTO>>(data);
                if (data is null)
                    throw new Exception("Data Not Found");
                generalResponse.Data = data;
                generalResponse.Success = true;
                return Ok(generalResponse);
            }
            catch (Exception ex)
            {
                generalResponse.Message = ex.Message;
                return BadRequest(generalResponse);
            }
        }
        [HttpGet("{id:int}")]
        public ActionResult<GeneralResponseDTO> GetById(int id)
        {
            try
            {
                var data = _dbContext.Coupons.FirstOrDefault(c => c.CouponId == id); 
                if (data is null)
                    throw new Exception("Data Not Found");
                var result = _mapper.Map<CouponDTO>(data);          
                generalResponse.Data = data;
                generalResponse.Success = true;
                return Ok(generalResponse);
            }
            catch (Exception ex)
            {
                generalResponse.Message = ex.Message;
                return BadRequest(generalResponse);
            }
        }
        [HttpGet("GetByCode/{code:regex(^[[a-zA-Z0-9]]+$)}")]
        public ActionResult<GeneralResponseDTO> GetByCode(string code)
        {
            try
            {
                var data = _dbContext.Coupons.FirstOrDefault(c => c.CouponCode == code);
                var result = _mapper.Map<CouponDTO>(data);
                if (data is null)
                    throw new Exception("Data Not Found");
                generalResponse.Data = data;
                generalResponse.Success = true;
                return Ok(generalResponse);
            }
            catch (Exception ex)
            {
                generalResponse.Message = ex.Message;
                return BadRequest(generalResponse);
            }
        }
        [HttpPost]
        public IActionResult AddCoupon(CouponDTO coupon)
        {
            try
            {
                if (coupon == null)
                {
                    throw new ArgumentNullException(nameof(coupon), "Coupon data is null");
                }

                var result = _mapper.Map<CouponModel>(coupon);
                if (result == null)
                    throw new Exception("Data Not Valid");
                _dbContext.Coupons.Add(result);
                _dbContext.SaveChanges();
                generalResponse.Success = true;
                generalResponse.Data = result;

                // Returns a 201 status code with a location header pointing to the GetById method.
                return CreatedAtAction(nameof(GetById), new { id = result.CouponId }, generalResponse);
            }
            catch (Exception ex)
            {
                generalResponse.Success = false;
                generalResponse.Message = ex.Message;
                return BadRequest(generalResponse);
            }
        }
        [HttpPut]
        public ActionResult<GeneralResponseDTO> EditCoupon(CouponDTO coupon)
        {
            try
            {
                if (coupon == null)
                {
                    throw new ArgumentNullException(nameof(coupon), "Coupon data is null");
                }

                var result = _mapper.Map<CouponModel>(coupon);
                if (result == null)
                    throw new Exception("Data Not Valid");
                _dbContext.Coupons.Update(result);
                _dbContext.SaveChanges();
                generalResponse.Success = true;
                generalResponse.Data = result;

                // Returns a 201 status code with a location header pointing to the GetById method.
               return generalResponse;  
            }
            catch (Exception ex)
            {
                generalResponse.Success = false;
                generalResponse.Message = ex.Message;
                return BadRequest(generalResponse);
            }
        }
        [HttpDelete]
        public IActionResult RemoveCoupon(int id)
        {
            try
            {
                var data = _dbContext.Coupons.FirstOrDefault(c => c.CouponId == id);
                if (data is null)
                    throw new Exception("Data Not Found");
                _dbContext.Coupons.Remove(data);
                _dbContext.SaveChanges();
                generalResponse.Success = true; 
                return Ok(generalResponse);
            }
            catch (Exception ex)
            {
                generalResponse.Message = ex.Message;
                return BadRequest(generalResponse);
            }
        }
    }
}