using Manog.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Manog.Services.ProductModelAPI.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<ProductModel> Coupons => Set<ProductModel>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
