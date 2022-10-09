using System.ComponentModel.DataAnnotations.Schema;

namespace Cripto_Api.Data
{
    public class Coin
    {
        public int Id { get; set; }
        public string CoinName { get; set; }
        public double CoinAmount { get; set; }
        public string Image { get; set; }

        [ForeignKey(nameof(Wallet))]
        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }
    }
}
