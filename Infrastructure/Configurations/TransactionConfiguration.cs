using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Configurations
{
    public class TransactionConfiguration
    {
        public TransactionConfiguration(EntityTypeBuilder<Domain.Entities.Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.Property(p => p.UserId).HasColumnName("USER_ID");

            builder.Property(p => p.Amount).HasColumnName("AMOUNT");

            builder.Property(p => p.WalletId).HasColumnName("WALLET_ID");

            builder.Property(p => p.Description).HasColumnName("DESCRIPTION");

            builder.Property(p => p.CreatedAt).HasColumnName("CREATED_AT");

            builder.Property(p => p.CreatedBy).HasColumnName("CREATED_BY");

            builder.Property(p => p.UpdatedAt).HasColumnName("UPDATED_AT");

            builder.Property(p => p.UpdatedBy).HasColumnName("UPDATED_BY");

            builder.Property(p => p.DeletedAt).HasColumnName("DELETED_AT");

            builder.Property(p => p.DeletedBy).HasColumnName("DELETED_By");
        }
    }
}