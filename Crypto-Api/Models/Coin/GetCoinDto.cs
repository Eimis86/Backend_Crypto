namespace Cripto_Api.Models.Coin
{
    public class GetCoinDto : CoinDto
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string CoinName { get; set; }
        public double CoinAmount { get; set; }
        public int WalletId { get; set; }
    }
}
