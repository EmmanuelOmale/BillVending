using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Infrastructure
{
    public class IdentityServiceDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public IdentityServiceDbContext(DbContextOptions<IdentityServiceDbContext> options) : base (options) {}
        public DbSet<AppUser> AppUsers { get; set; }
    }
}