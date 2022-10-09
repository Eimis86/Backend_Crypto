
using Cripto_Api.Models.Wallet;

namespace Cripto_Api.Models.User
{
    public class UserDto : BaseUserDto
    {
        public int Id { get; set; }
        public virtual IList<WalletDto> Wallets { get; set; }
    }
}
