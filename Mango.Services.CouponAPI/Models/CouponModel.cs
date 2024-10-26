﻿using System.ComponentModel.DataAnnotations;

namespace Mango.Services.CouponAPI.Models
{
    public class CouponModel
    {
        [Key]
        public int CouponId { get; set; }
        [Required]
        public string CouponCode { get; set; }
        [Required]
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}