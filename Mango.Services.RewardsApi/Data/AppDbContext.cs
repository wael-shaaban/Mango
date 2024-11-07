using Mango.Services.RewardsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.RewardsApi.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<Rewards> Rewards => Set<Rewards>();
    }
}
