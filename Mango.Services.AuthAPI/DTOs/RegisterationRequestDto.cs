namespace Mango.Services.AuthAPI.DTOs
{
    public record RegisterationRequestDto(string Name, string Email, string PhoneNumber, string Password, string Address, string? Role);
}