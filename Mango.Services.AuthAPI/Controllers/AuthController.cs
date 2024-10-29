using Mango.Services.AuthAPI.DTOs;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly GeneralResponseDto generalResponseDto = new GeneralResponseDto();
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
            if (loginResponse.User is null && string.IsNullOrEmpty(loginResponse.Token))
            {
                generalResponseDto.Success = false;
                generalResponseDto.Message = "UserName or Password Is Invalid";
                return BadRequest(generalResponseDto);
            }
            generalResponseDto.Data = loginResponse;
            return Ok(loginResponse);
        }
        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole(RegisterationRequestDto loginRequestDto)
        {
            var loginResponse = await authService.AddRole(loginRequestDto.Email, loginRequestDto.Role);
            if (!string.IsNullOrEmpty(loginResponse))
            {
                generalResponseDto.Success = false;
                generalResponseDto.Message = loginResponse;
                return BadRequest(generalResponseDto);
            }
            generalResponseDto.Data = "Successfully adding role";
            return Ok(loginResponse);
        }
    }
}