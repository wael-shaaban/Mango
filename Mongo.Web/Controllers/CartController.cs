using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mongo.Web.Services.IServices;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Mongo.Web.Controllers
{
    public class CartController(IShoppingCartService cartService) : Controller
    {
        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartDtoBasedLoggedInUser());
        }
        [HttpGet]
        public async Task<IActionResult> Remove(int cartDetailsId)
        {
            if (ModelState.IsValid)
            {
                //var userId = User.Claims.Where(c => c.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
                var resonseDto = await cartService.RemoveFromCartAsync(cartDetailsId);
                if (resonseDto is not null && resonseDto.Success)
                {
                    TempData["success"] = "successfully removing product form cart";
                    return RedirectToAction(nameof(CartIndex));
                }
            }
            return View();
        }
         [HttpPost]
        public async Task<IActionResult> EmailCart(CartDto cartDto)
        {
            if (ModelState.IsValid)
            {
                var cart = await LoadCartDtoBasedLoggedInUser();
                cart.CartHeader.Email = User.Claims.Where(c => c.Type == JwtRegisteredClaimNames.Email)?.FirstOrDefault()?.Value;
                
                var resonseDto = await cartService.CartEmail(cart);
                if (resonseDto is not null && resonseDto.Success)
                {
                    TempData["success"] = "email will be processes and sent sholry!";
                    return RedirectToAction(nameof(CartIndex));
                }
            }
            return View();
        }
        

         [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
        {
            if (ModelState.IsValid)
            {
                //var userId = User.Claims.Where(c => c.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
                var resonseDto = await cartService.ApplyCouponAsync(cartDto);
                if (resonseDto is not null && resonseDto.Success)
                {
                    TempData["success"] = "successfully appling coupon on product's cart";
                    return RedirectToAction(nameof(CartIndex));
                }
            }
            return View();
        }
        
         [HttpPost]
        public async Task<IActionResult> RemoveCoupon(CartDto cartDto)
        {
            if (ModelState.IsValid)
            {
                cartDto.CartHeader.CouponCode = "";
                var resonseDto = await cartService.ApplyCouponAsync(cartDto);
                if (resonseDto is not null && resonseDto.Success)
                {
                    TempData["success"] = "successfully deleting coupon on product's cart";
                    return RedirectToAction(nameof(CartIndex));
                }
            }
            return View();
        }

        private async Task<CartDto?> LoadCartDtoBasedLoggedInUser()
        {
            var userId = User.Claims.Where(c => c.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            var resonseDto = await cartService.GetCartByUserIdAsync(userId);
            //  return resonseDto is not null && resonseDto.Success ?
            //    JsonConvert.DeserializeObject<CartDto>(Convert.ToString(resonseDto.Data)) : new CartDto();
            if (resonseDto is not null && resonseDto.Success)
            {
                var data =  JsonConvert.DeserializeObject<CartDto>(Convert.ToString(resonseDto.Data));
                return data;
            }
            return new CartDto();
        }
    }
}