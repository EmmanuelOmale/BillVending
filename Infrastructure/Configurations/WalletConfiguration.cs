using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;

namespace Infrastructure.Configurations
{
    public class WalletConfiguration
    {
        public WalletConfiguration(EntityTypeBuilder<Domain.Wallet> builder)
        {
            builder.ToTable("Wallets");

            builder.Property(p => p.UserId).HasColumnName("USER_ID");

            builder.Property(p => p.Balance).HasColumnName("WALLET_BALANCE");

            builder.Property(p => p.CreatedAt).HasColumnName("CREATED_AT");

            builder.Property(p => p.CreatedBy).HasColumnName("CREATED_BY");

            builder.Property(p => p.UpdatedAt).HasColumnName("UPDATED_AT");

            builder.Property(p => p.UpdatedBy).HasColumnName("UPDATED_BY");

            builder.Property(p => p.DeletedAt).HasColumnName("DELETED_AT");

            builder.Property(p => p.DeletedBy).HasColumnName("DELETED_By");
        }
    }
}