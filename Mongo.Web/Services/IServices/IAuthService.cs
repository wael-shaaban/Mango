namespace Mongo.Web.Services.IServices
{
    public interface IAuthService
    {
        Task<GeneralResponseDTO> RegisterAsync(RegistrationRequestDto registerationRequestDto);
        Task<GeneralResponseDTO> LoginAsync(LoginRequestDto loginRequestDto);
        Task<GeneralResponseDTO> AssignRoleAsync(RegistrationRequestDto registerationRequestDto);
    }
}