namespace Cripto_Api.Models.Coin
{
    public class CreateCoinDto : CoinDto
    {
        public string Image { get; set; }
        public string CoinName { get; set; }
        public double CoinAmount { get; set; }
        public int WalletId { get; set; }
    }
}
