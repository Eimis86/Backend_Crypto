namespace Cripto_Api.Models.Wallet
{
    public class GetWalletsDto : BaseWalletDto
    {
        public int UserId { get; set; }
        public int Id { get; set; }
    }
}
