using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Services.IServices;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mango.Services.AuthAPI.Services
{
    public class JwtGeneratorService(IOptions<JwtOptions> options) : IJwtGeneratorService
    {
        public string GenerateToken(AppUser appUser,IEnumerable<string> roles)
        {
            var jwtOptions = options.Value;
            var secret = jwtOptions.Secret;
            var issuer = jwtOptions.Issuer;
            var audience = jwtOptions.Audience;
            if (secret is null || issuer is null || audience is null)
                throw new ApplicationException("Jwt is not set in the configuration");
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var tokenHandler = new JwtSecurityTokenHandler();
            var claimList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
                new Claim(JwtRegisteredClaimNames.Sub, appUser.Id),
                new Claim(JwtRegisteredClaimNames.Name, appUser.UserName)
            };
            //foreach (var role in roles)
            //    claimList.Add(new Claim("Role", role));
            //claimList.Union(roles.Select(role => new Claim("Role", role)));
            claimList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }
    }
}