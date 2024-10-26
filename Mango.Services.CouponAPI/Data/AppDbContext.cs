using Mango.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions):DbContext(dbContextOptions)
    {
       public DbSet<CouponModel> Coupons => Set<CouponModel>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
