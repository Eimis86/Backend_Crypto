using Cripto_Api.Contract;
using Cripto_Api.Data;
using Cripto_Api.Models.Coin;
using Cripto_Api.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Cripto_Api.Repository
{
    public class CoinRepository : GenericRepository<Coin>, ICoinContract
    {
        private readonly CriptoAPIDbContext _context;
        private readonly IConfiguration _configuration;
        public CoinRepository(IConfiguration configuration, CriptoAPIDbContext context) : base(context)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<Coin> InsertCoin(Coin coin)
        {
            await _context.Coins.AddAsync(coin);
            await _context.SaveChangesAsync();

            return null;
        }

        public async Task<Coin> GetCoinId(CreateCoinDto createCoinDto)
        {
            var coinId = await _context.Coins
                .Where( x => x.WalletId == createCoinDto.WalletId)
                .Where( x => x.CoinName == createCoinDto.CoinName)
                .FirstOrDefaultAsync();
            
            return coinId;
        }

        public async Task<Coin> AddPrice (CreateCoinDto createCoinDto)
        {
            var coinId = await _context.Coins
                .Where(x => x.WalletId == createCoinDto.WalletId)
                .Where(x => x.CoinName == createCoinDto.CoinName)
                .FirstOrDefaultAsync();

            coinId.CoinAmount += createCoinDto.CoinAmount;

            return coinId;
        }

        public async Task<Coin> RemoveCoin(SellCoinDto sellCoinDto)
        {
            var coin = await _context.Coins
                .Where(x => x.WalletId == sellCoinDto.WalletId)
                .Where(x => x.CoinName == sellCoinDto.CoinName)
                .FirstOrDefaultAsync();

            coin.CoinAmount -= sellCoinDto.CoinAmount;

            if(coin.CoinAmount < 0)
            {
                return null;
            }

            return coin;
        }
        public async Task<List<Coin>> FindAllCoinsInWallet(int walletId)
        {
            var coins = await _context.Coins.Where(x => x.WalletId == walletId).ToListAsync();

            return coins;
        }
    }
}
