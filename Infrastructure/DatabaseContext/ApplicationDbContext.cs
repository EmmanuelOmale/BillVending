using Microsoft.EntityFrameworkCore;
using Infrastructure.Configurations;
using Domain;

namespace Infrastructure.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {
            
        }
        public DbSet<Wallet> Wallets { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new WalletConfiguration(modelBuilder.Entity<Wallet>());
        }

    }
}