namespace Mango.Services.AuthAPI.DTOs
{
    public record LoginResponseDto(UserDto User, string Token);
}