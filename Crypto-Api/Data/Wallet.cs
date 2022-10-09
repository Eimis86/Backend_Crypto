using System.ComponentModel.DataAnnotations.Schema;

namespace Cripto_Api.Data
{
    public class Wallet
    {
        public int Id { get; set; }
        public string WalletNumber { get; set; }
        public string WalletName { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }
        public virtual ICollection<Coin> Coins { get; set; }

    }
}
