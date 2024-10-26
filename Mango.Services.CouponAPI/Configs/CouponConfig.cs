using Mango.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mango.Services.CouponAPI.Configs
{
    public class CouponConfig : IEntityTypeConfiguration<CouponModel>
    {
        public void Configure(EntityTypeBuilder<CouponModel> builder)
        {
            builder.HasData(new List<CouponModel>
            {
                new CouponModel { CouponId = 1, CouponCode = "DISCOUNT10", DiscountAmount = 10.0, MinAmount = 100 },
                new CouponModel { CouponId = 2, CouponCode = "SAVE15", DiscountAmount = 15.0, MinAmount = 150 },
                new CouponModel { CouponId = 3, CouponCode = "SPRING20", DiscountAmount = 20.0, MinAmount = 200 },
                new CouponModel { CouponId = 4, CouponCode = "SUMMER25", DiscountAmount = 25.0, MinAmount = 250 },
                new CouponModel { CouponId = 5, CouponCode = "FALL30", DiscountAmount = 30.0, MinAmount = 300 },
                new CouponModel { CouponId = 6, CouponCode = "WINTER35", DiscountAmount = 35.0, MinAmount = 350 },
                new CouponModel { CouponId = 7, CouponCode = "NEWYEAR40", DiscountAmount = 40.0, MinAmount = 400 },
                new CouponModel { CouponId = 8, CouponCode = "HOLIDAY45", DiscountAmount = 45.0, MinAmount = 450 },
                new CouponModel { CouponId = 9, CouponCode = "FLASH50", DiscountAmount = 50.0, MinAmount = 500 },
                new CouponModel { CouponId = 10, CouponCode = "SUPER55", DiscountAmount = 55.0, MinAmount = 550 }
            });
        }
    }
}