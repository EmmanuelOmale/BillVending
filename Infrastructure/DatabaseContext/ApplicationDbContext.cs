using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Infrastructure.Configurations;
using Domain.Entities;
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

        public DbSet<Domain.Entities.Wallet> Wallets { get; set; }
        public DbSet<Domain.Entities.Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.AddInterceptors(_auditableInterceptor);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new WalletConfiguration(modelBuilder.Entity<Domain.Entities.Wallet>());
            new TransactionConfiguration(modelBuilder.Entity<Domain.Entities.Transaction>());
        }

    }
}