using Mango.Services.ShoppingCartAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ShoppingCartAPI.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<CartHeaderModel> CartHeaders => Set<CartHeaderModel>();
        public DbSet<CartDetailsModel> CartDetails => Set<CartDetailsModel>();
    }
}
