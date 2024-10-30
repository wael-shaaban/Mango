using Mango.Services.AuthAPI.DTOs;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService authService, SignInManager<AppUser> signInManager) : ControllerBase
    {
        protected GeneralResponseDto generalResponseDto = new GeneralResponseDto();
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterationRequestDto registerationRequest)
        {
            var errorMessage = await authService.Register(registerationRequest);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                generalResponseDto.Success = false;
                generalResponseDto.Message = errorMessage;
                return BadRequest(generalResponseDto);
            }
            return Ok(generalResponseDto);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var loginResponse = await authService.Login(loginRequestDto);
            if (loginResponse.User is null)
            {
                generalResponseDto.Success = false;
                generalResponseDto.Message = "UserName or Password Is Invalid";
                return BadRequest(generalResponseDto);
            }
            var result = await signInManager.PasswordSignInAsync(loginRequestDto.UserName, loginRequestDto.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                // Handle successful login
                //return RedirectToAction(nameof(HomeController.Index), "Home");
                generalResponseDto.Success = true;
                generalResponseDto.Data = loginResponse;
                return Ok(generalResponseDto);
            }
            if (result.RequiresTwoFactor)
            {
                // Handle two-factor authentication case
            }
            if (result.IsLockedOut)
            {
                // Handle lockout scenario
            }
            else
            {
                // Handle failure
                //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                generalResponseDto.Success = false;
                generalResponseDto.Message = "Invalid login attempt.";
                return BadRequest(generalResponseDto);
            }
            generalResponseDto.Success = true;
            generalResponseDto.Data = loginResponse;

            return Ok(loginResponse);
        }
        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole(RegisterationRequestDto loginRequestDto)
        {
            var assignRole = await authService.AddRole(loginRequestDto.Email, loginRequestDto.Role.ToUpper());
            if (!string.IsNullOrEmpty(assignRole))
            {
                generalResponseDto.Success = false;
                generalResponseDto.Message = assignRole;
                return BadRequest(generalResponseDto);
            }
            //generalResponseDto.Data = "Successfully adding role";
            return Ok(generalResponseDto);
        }
    }
}
/*services.ConfigureApplicationCookie(options => 
{
     options.Cookie.HttpOnly = true;
     options.Cookie.Expiration = TimeSpan.FromDays(5);
     options.LoginPath = "/Account/Login";
 });*/