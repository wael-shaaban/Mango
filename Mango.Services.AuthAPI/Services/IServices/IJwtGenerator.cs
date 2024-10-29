using Mango.Services.AuthAPI.Models;
using Microsoft.Extensions.Configuration;

namespace Mango.Services.AuthAPI.Services.IServices
{
    public interface IJwtGeneratorService
    {
        string GenerateToken(AppUser appUser);
    }
}