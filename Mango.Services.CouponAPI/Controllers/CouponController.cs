using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController(AppDbContext _dbContext) : ControllerBase
    {
        [HttpGet]
        public ActionResult<CouponModel> GetAllCoupons() => Ok(_dbContext.Coupons.ToList());
        [HttpGet("{id:int}")]
        public ActionResult<CouponModel> GetById(int id)
        {
            try
            {
                var data = _dbContext.Coupons.Find(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
