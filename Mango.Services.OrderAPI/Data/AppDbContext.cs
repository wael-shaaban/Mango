using Microsoft.EntityFrameworkCore;
using MMango.Services.OrderAPI;

namespace Mango.Services.OrderAPI.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<OrderHeaderDto> CartHeaders => Set<OrderHeaderDto>();
        public DbSet<OrderDetailsDto> CartDetails => Set<OrderDetailsDto>();
    }
}
