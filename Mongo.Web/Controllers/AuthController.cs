using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Mongo.Web.Services.IServices;
using Mongo.Web.Utility;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Mongo.Web.Controllers
{
    //SAad@#1234
    public class AuthController(IAuthService authService,ITokeProvider tokeProvider) : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{Text = SD.RoleAdmin,Value = SD.RoleAdmin},
                new SelectListItem{Text = SD.RoleCustomer,Value = SD.RoleCustomer}
            };
            ViewBag.Roles = list;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto registerationRequest)
        {
            string error = "";
            if (ModelState.IsValid)
            {
                var result=await authService.RegisterAsync(registerationRequest);
                GeneralResponseDTO assingRole;
               if(authService is not null && result.Success)
                {
                   if(string.IsNullOrEmpty(registerationRequest.Role))
                        registerationRequest.Role =  SD.RoleCustomer;
                    assingRole = await authService.AssignRoleAsync(registerationRequest);
                    if(assingRole is not null && assingRole.Success)
                    {
                        TempData["success"] = "successfully registeration!";
                        return RedirectToAction(nameof(Login));
                    }
                    
                }else
                        error = result.Message;
            }
            var list = new List<SelectListItem>
            {
                new SelectListItem{Text = SD.RoleAdmin,Value = SD.RoleAdmin},
                new SelectListItem{Text = SD.RoleCustomer,Value = SD.RoleCustomer}
            };
            ViewBag.Roles = list;
            TempData["error"] = "try again please!, "+error;
            return View(registerationRequest);
        }
        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestobj)
        {
            string error = "";
            if (ModelState.IsValid)
            {
                var result = await authService.LoginAsync(loginRequestobj);
                if (authService is not null && result.Success)
                {
                    LoginResponseDto loginRequestDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(result.Data));
                    await SigninUser(loginRequestDto);
                    tokeProvider.SetToken(loginRequestDto.Token);
                    return RedirectToAction("Index","Home");
                }
                else error= result.Message;
            }
            TempData["error"] = "error encoutered!"+error;
            //ModelState.AddModelError("CustomError","invalid username or password");
            return View(loginRequestobj);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            tokeProvider.ClearToken();
            return RedirectToAction("Index", "Home");
        }

        private async Task SigninUser(LoginResponseDto loginResponseDto)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwt = jwtHandler.ReadJwtToken(loginResponseDto.Token);
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));
            identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));
            var claimPrincple = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincple);
        }
    }
}
