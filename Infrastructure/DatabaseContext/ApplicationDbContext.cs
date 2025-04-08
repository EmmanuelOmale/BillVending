using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Infrastructure.Configurations;
using Domain;
using Common.Interceptors;

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

        public DbSet<Domain.Wallet> Wallets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.AddInterceptors(_auditableInterceptor);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new WalletConfiguration(modelBuilder.Entity<Domain.Wallet>());
        }

    }
}