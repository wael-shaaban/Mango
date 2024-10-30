using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.DTOs;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Mango.Services.AuthAPI.Services
{
    public class AuthService(AppDbContext appDbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager
        , IJwtGeneratorService jwtGeneratorService) : IAuthService
    {


        public async Task<LoginResponseDto> Login(LoginRequestDto requestDto)
        {
            //var userExist = await userManager.FindByEmailAsync(requestDto.UserName.ToLower());
            var userExist = await appDbContext.AppUsers.FirstOrDefaultAsync(c => c.Email.ToLower() == requestDto.UserName.ToLower());
            if (userExist is not null && await userManager.CheckPasswordAsync(userExist, requestDto.Password))
            {
                var roles =await userManager.GetRolesAsync(userExist);
                return new LoginResponseDto(
                    User:
                    new UserDto
                    {
                        Address = userExist.Address,
                        Email = userExist.Email,
                        ID = userExist.Id,
                        Name = userExist.FName + " " + userExist.LName,
                        PhoneNumber = userExist.PhoneNumber
                    },
                   Token:
                   jwtGeneratorService.GenerateToken(userExist,roles));
            }
            else
                return new LoginResponseDto(null, string.Empty);
        }

        public async Task<string> Register(RegisterationRequestDto requestDto)
        {
            StringBuilder errors = new StringBuilder();
            //setup user
            AppUser user = new()
            {
                Email = requestDto.Email,
                UserName = requestDto.Email,
                NormalizedEmail = requestDto.Email.ToUpper(),
                FName = requestDto.Name.Split(' ')[0],
                LName = requestDto.Name.Split(' ')[1],
                PhoneNumber = requestDto.PhoneNumber,
                Address = requestDto.Address
            };
            try
            {
                var userExist = await userManager.FindByEmailAsync(user.Email);
                if (userExist is null)
                {
                    var result = await userManager.CreateAsync(user, requestDto.Password);
                    if (result is not null && result.Succeeded)
                    {
                        var userresult = await appDbContext.AppUsers.FirstAsync(c => c.UserName == requestDto.Email);
                        UserDto userDto = new()
                        {
                            Email = userresult?.Email,
                            ID = userresult?.Id,
                            Name = userresult?.FName + " " + userresult?.LName,
                            PhoneNumber = userresult?.PhoneNumber,
                            Address = userresult?.Address
                        };
                        return string.Empty;
                    }
                    else
                    {
                        foreach (var error in result?.Errors)
                            errors.AppendLine(error.Description);
                        return errors.ToString();
                    }
                }
                errors.AppendLine("user is aleadey exist");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            if(errors.Length > 0)
                return errors.ToString();   
            return string.Empty;
        }

        public  async Task<string> AddRole(string email, string roleName)
        {
            StringBuilder errors = new StringBuilder();
            var userExist = appDbContext.AppUsers.FirstOrDefault(c => c.Email.ToLower() == email.ToLower());
            if (userExist is not null)
            {
                IdentityResult result = default;
                if (!roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                    result = roleManager.CreateAsync(new IdentityRole { Name = roleName }).GetAwaiter().GetResult();
                //if (result?.Succeeded==true)
                //{
                var result2 = await userManager.AddToRoleAsync(userExist, roleName);
                    if (result2.Succeeded)
                        return string.Empty;
                    foreach (var error in result2?.Errors)
                        errors.AppendLine(error.Description);
                //}
                //foreach (var error in result?.Errors)
                //    errors.AppendLine(error.Description);
            }
           
            if (errors.Length > 0)
                return errors.ToString();
            return string.Empty;
        }
    }
}