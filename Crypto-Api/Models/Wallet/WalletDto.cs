using Cripto_Api.Models.Coin;
using Cripto_Api.Models.User;

namespace Cripto_Api.Models.Wallet
{
    public class WalletDto : BaseWalletDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public virtual IList<CoinDto> Coinsdto { get; set; }
    }
}
