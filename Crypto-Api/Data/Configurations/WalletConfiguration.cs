using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cripto_Api.Data.Configurations
{
    public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.HasData(
                new Wallet
                {
                    Id = 1,
                    WalletNumber = "A22asd42asd",
                    WalletName = "BigTech Wallet",
                    UserId = 1,
                }
             );

        }
    }
}
