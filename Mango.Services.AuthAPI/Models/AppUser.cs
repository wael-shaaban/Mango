using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Models
{
    public class AppUser:IdentityUser
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string Address { get; set; }
    }
}
