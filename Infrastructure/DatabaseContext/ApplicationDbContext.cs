using Microsoft.EntityFrameworkCore;
using Domain;

namespace Infrastructure.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {
            
        }
        public DbSet<Wallet> Wallets { get; set; }
    }
}