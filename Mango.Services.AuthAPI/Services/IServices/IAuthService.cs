using Mango.Services.AuthAPI.DTOs;

namespace Mango.Services.AuthAPI.Services.IServices
{
    public interface IAuthService
    {
        Task<string> Register(RegisterationRequestDto requestDto);
        Task<LoginResponseDto> Login(LoginRequestDto requestDto);
        Task<string> AddRole(string userName,string roleName);
    }
}