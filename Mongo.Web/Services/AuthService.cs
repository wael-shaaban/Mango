using Mongo.Web.Services.IServices;
using Mongo.Web.Utility;

namespace Mongo.Web.Services
{
    public class AuthService(IBaseService _baseService) : IAuthService
    {
        public async Task<GeneralResponseDTO> AssignRoleAsync(RegistrationRequestDto registerationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.POST,
                Url = SD.AuthApiBaseUrl + "/api/auth/AssignRole",
                Data = registerationRequestDto
            }, false);
        }

        public async Task<GeneralResponseDTO> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.POST,
                Url = SD.AuthApiBaseUrl + "/api/auth/login",
                Data = loginRequestDto
            }, false);
        }

        public async Task<GeneralResponseDTO> RegisterAsync(RegistrationRequestDto registerationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.POST,
                Url = SD.AuthApiBaseUrl + "/api/auth/register",
                Data = registerationRequestDto
            }, false);
        }
    }
}