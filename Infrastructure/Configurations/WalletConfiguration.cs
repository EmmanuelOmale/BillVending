using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;


namespace Infrastructure.Configurations
{
    public class WalletConfiguration
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {

        }
    }
}