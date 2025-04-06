using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Infrastructure.Configurations;
using Domain;
using Infrastructure.Interceptors;

namespace Infrastructure.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        private readonly AuditableEntitySaveChangesInterceptor _auditableInterceptor;

        public ApplicationDbContext(
                                    DbContextOptions<ApplicationDbContext> options, 
                                    AuditableEntitySaveChangesInterceptor auditableInterceptor) : base(options)
        {
            _auditableInterceptor = auditableInterceptor;
        }

        public DbSet<Wallet> Wallets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.AddInterceptors(_auditableInterceptor);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new WalletConfiguration(modelBuilder.Entity<Wallet>());
        }

    }
}