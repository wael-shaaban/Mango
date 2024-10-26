using Mango.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions):DbContext(dbContextOptions)
    {
        DbSet<CouponModel> Coupons => Set<CouponModel>();
    }
}
