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

        private async Task<CartDto?> LoadCartDtoBasedLoggedInUser()
        {
            var userId = User.Claims.Where(c => c.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            var resonseDto = await cartService.GetCartByUserIdAsync(userId);
            return resonseDto is not null && resonseDto.Success ?
                JsonConvert.DeserializeObject<CartDto>(Convert.ToString(resonseDto.Data)) : new CartDto();
        }
    }
}