using System.ComponentModel.DataAnnotations;

namespace Mongo.Web.ViewModels
{
    public class LoginRequestDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

    }
}