using System.ComponentModel.DataAnnotations.Schema;

namespace Cripto_Api.Data
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Role { get; set; }
        //public string Token { get; set; }
        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}
