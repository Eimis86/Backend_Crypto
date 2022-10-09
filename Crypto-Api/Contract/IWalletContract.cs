using Cripto_Api.Data;
using Cripto_Api.Models.Wallet;

namespace Cripto_Api.Contract
{
    public interface IWalletContract : IGenericContract<Wallet>
    {
        Task<List<Wallet>> GetWallets(int userid);
        Task<bool> GetCount(int userid);
    }

}
