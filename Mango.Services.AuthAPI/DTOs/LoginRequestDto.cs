using System.ComponentModel.DataAnnotations;

namespace Mango.Services.AuthAPI.DTOs
{
    public record LoginRequestDto([Required]string UserName, [Required]string Password);
}