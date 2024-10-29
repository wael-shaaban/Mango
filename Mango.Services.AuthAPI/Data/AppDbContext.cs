using Mango.Services.AuthAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.AuthAPI.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : IdentityDbContext<AppUser>(dbContextOptions)
    {
        public DbSet<AppUser> AppUsers => Set<AppUser>();
    }
}
