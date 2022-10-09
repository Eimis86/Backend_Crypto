using Cripto_Api.Data;
using Cripto_Api.Models.User;

namespace Cripto_Api.Contract
{
    public interface IUserContract : IGenericContract<User>
    {
        Task<User> GetUserById(int id);
        Task<UserResponseTokenDto> Login(LoginUserDto loginUserDto);
        Task<User> Register(User user);
        Task<User> UpdatePass(UpdateUserDto updateUserDto, User user);
    }
}