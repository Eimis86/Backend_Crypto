using Cripto_Api.Contract;
using Cripto_Api.Data;
using Cripto_Api.Models.Wallet;
using Microsoft.EntityFrameworkCore;

namespace Cripto_Api.Repository
{
    public class WalletRepository : GenericRepository<Wallet>, IWalletContract
    {
        private readonly CriptoAPIDbContext _context;
        public WalletRepository( CriptoAPIDbContext database) : base(database)
        {
            _context = database;
        }

        public async Task<List<Wallet>> GetWallets(int userid)
        {
           return await _context.Wallets
                .Where( x => x.UserId == userid )
                .ToListAsync();
        }

        public async Task<bool> GetCount(int userId)
        {
            if(await _context.Wallets
                .Where(x => x.UserId == userId)
                .CountAsync() >= 5)
            {
                return true;
            }
            return false;
        }
    }
}
