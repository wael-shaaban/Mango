﻿using System.ComponentModel.DataAnnotations;

namespace Mongo.Web.ViewModels
{
    public class RegistrationRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Address { get; set; }
        public string? Role { get; set; }
    }
}