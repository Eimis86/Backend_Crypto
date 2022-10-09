namespace Cripto_Api.Models.Coin
{
    public class SellCoinDto : CoinDto
    {
        public string CoinName { get; set; }
        public double CoinAmount { get; set; }
        public int WalletId { get; set; }
    }
}
