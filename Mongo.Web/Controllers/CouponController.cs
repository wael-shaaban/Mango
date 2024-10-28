using Microsoft.AspNetCore.Mvc;
using Mongo.Web.Services.IServices;
using Mongo.Web.ViewModels;
using Newtonsoft.Json;

namespace Mongo.Web.Controllers
{
    public class CouponController(ICouponService couponService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDTO> coupons = new();
            GeneralResponseDTO result = await couponService.GetAllCouponsAsync();
            if (result is not null)
            {
                if (result.Success)
                {
                    TempData["success"] = "Getting all coupons successfully!";
                    coupons = JsonConvert.DeserializeObject<List<CouponDTO>>(Convert.ToString(result.Data));
                }
                else
                    TempData["error"] = result.Message;
            }
            return View(coupons);
        }
        [HttpGet]
        public async Task<IActionResult> CouponCreate() => View();
        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDTO couponDTO)
        {
            if (ModelState.IsValid)
            {
                var response = await couponService.CreateCouponAsync(couponDTO);
                if (response is not null)
                {
                    if (response.Success)
                    {
                        TempData["success"] = "Creating New coupon successfully!";
                        return RedirectToAction(nameof(CouponIndex));
                    }
                    else
                        TempData["error"] = response.Message;
                }
            }
            return View(couponDTO);
        }
        [HttpGet]
        public async Task<IActionResult> CouponDelete(int couponId)
        {
            CouponDTO coupon = new();
            GeneralResponseDTO result = await couponService.GetCouponByIdAsync(couponId);
            if (result is not null)
            {
                if (result.Success)
                {
                    TempData["success"] = "Updating coupon successfully!";
                    coupon = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(result.Data));
                }
                else TempData["error"] = result.Message;
                return View(coupon);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDTO couponDTO)
        {
            GeneralResponseDTO result = await couponService.DeleteCouponAsync(couponDTO.CouponId);
            if (result is not null)
            {
                if (result.Success)
                {
                    TempData["success"] = "Deleting coupon successfully!";
                    return RedirectToAction(nameof(CouponIndex));
                }
                else TempData["error"] = result.Message;
            }
            return View(couponDTO);
        }
    }
}