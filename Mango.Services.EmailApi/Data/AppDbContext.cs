using Mango.Services.EmailApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.EmailApi.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<EmailLogger> Emails => Set<EmailLogger>();
    }
}
