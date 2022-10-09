using Cripto_Api.Data;
using Cripto_Api.Models.Coin;
using Cripto_Api.Models.User;

namespace Cripto_Api.Contract
{
    public interface ICoinContract:IGenericContract<Coin>
    {
        Task<Coin> InsertCoin(Coin coin);
        Task<Coin> GetCoinId(CreateCoinDto createCoinDto);
        Task<Coin> AddPrice(CreateCoinDto createCoinDto);
        Task<Coin> RemoveCoin(SellCoinDto sellCoinDto);
        Task<List<Coin>> FindAllCoinsInWallet(int walletId);
    }
}
