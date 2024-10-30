using Mongo.Web.Services.IServices;
using Mongo.Web.Utility;

namespace Mongo.Web.Services
{
    public class AuthService(IBaseService _baseService) : IAuthService
    {
        public Task<GeneralResponseDTO> AssignRoleAsync(RegistrationRequestDto registerationRequestDto)
        {
            return _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.POST,
                Url = SD.AuthApiBaseUrl + "/api/auth/AssignRole",
                Data = registerationRequestDto
            }, false);
        }

        public Task<GeneralResponseDTO> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.POST,
                Url = SD.AuthApiBaseUrl + "/api/auth/login",
                Data = loginRequestDto
            }, false);
        }

        public Task<GeneralResponseDTO> RegisterAsync(RegistrationRequestDto registerationRequestDto)
        {
            return _baseService.SendAsync(new RequestDTO
            {
                ApiType = SD.ApiType.POST,
                Url = SD.AuthApiBaseUrl + "/api/auth/register",
                Data = registerationRequestDto
            }, false);
        }
    }
}