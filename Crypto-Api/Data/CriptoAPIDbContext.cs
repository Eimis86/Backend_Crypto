using Cripto_Api.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Cripto_Api.Data
{
    public class CriptoAPIDbContext : DbContext
    {
        public CriptoAPIDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Coin> Coins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new WalletConfiguration());
            modelBuilder.ApplyConfiguration(new CoinConfiguration());
        }

    }
}
