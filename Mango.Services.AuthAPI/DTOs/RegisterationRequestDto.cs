using System.ComponentModel.DataAnnotations;

namespace Mango.Services.AuthAPI.DTOs
{
    public record RegisterationRequestDto([Required] string Name, [Required] string Email, [Required] string PhoneNumber, [Required] string Password, string Address, string? Role);
}